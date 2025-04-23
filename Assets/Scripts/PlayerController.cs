using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip bonusSound;
    public AudioClip powerShotSound;
    
    private AudioSource playerAudio;


    // Variables for displaying information on the screen
    public int bonusScore = 0;
    public Text bonusText;
    public int powerShots = 0;
    public Text powerShotsText;
    public int lives = 3;
    public Text livesText;

    //Shooting variables
    public GameObject bulletPrefab;

    // Variables for winning by distance
    public int currentLevel = 0;

    // Variables for winning on meat
    private int meatWinCondition = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        // Gain access to the field with the value of lives
        livesText = GameObject.Find("LivesLeft").GetComponent<Text>();
        // Gaining access to the field with the value of pavershots
        powerShotsText = GameObject.Find("PowerShots").GetComponent<Text>();
        // Gain access to the field with the value of points
        bonusText = GameObject.Find("BonusScore").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // If the space bar is pressed and the player is on the ground, then jump
        if ( Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // If ctrl is pressed and the number of powershots is greater than 0, subtract one powershots and release the projectile
        if (Input.GetKeyDown(KeyCode.LeftControl) && powerShots > 0 && !gameOver)
        {
            powerShots--;
            playerAudio.PlayOneShot(powerShotSound, 1.0f);
            Instantiate(bulletPrefab, transform.position + Vector3.up * 3.5f, bulletPrefab.transform.rotation);
        }

        // If the player presses the "Escape" key, go to the main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        // Display the number of lives on the screen
        livesText.text = "Lives: " + lives.ToString();

        // Display the number of points on the screen
        bonusText.text = "Meat collected: " + bonusScore.ToString();

        // Display the number of powershots on the screen
        powerShotsText.text = "Power Shots: " + powerShots.ToString();


    }

    // This method is called when the player dies
    private void GameOverTransition()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Bonus collision
        if (collision.gameObject.CompareTag("Bonus"))
        {
            playerAudio.PlayOneShot(bonusSound, 1.0f);
            Destroy(collision.gameObject);

            bonusScore++;
            powerShots++;
            
            if (bonusScore >= meatWinCondition)
            {
                gameOver = true; // Устанавливаем флаг завершения игры
                SceneManager.LoadScene("LevelTwoWin"); // Переход на сцену с победным экраном

            }
            
        }

        //Start distance win activator
        // This one was used to make sure that the player is able to win by distance
        // Later I realized another ways to do it, but come on, it's working so let it work
        if (collision.gameObject.CompareTag("DistanceWinTrigger"))
        {
            currentLevel = 1;
            Destroy(collision.gameObject);
        }


        // Ground collision
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            playerAudio.PlayOneShot(crashSound, 1.0f);
            Destroy(collision.gameObject);
            explosionParticle.Play();
            
            lives--; // Decrease the number of lives by 1

            if (lives > 0)
            {
                playerAudio.PlayOneShot(damageSound, 1.0f);

            }
            if (lives < 1)
            {
                gameOver = true;

                Debug.Log("Game Over!");
                playerAnim.SetBool("Death_b", true);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(deathSound, 1.0f);
                playerAnim.SetInteger("DeathType_int", 1);

                Invoke("GameOverTransition", 5f);   // Wait 5 seconds before going to the death screen
            }

            

            

        }
    }

}

