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
   

    // ���������� ��� ����������� ���������� �� ������
    public int bonusScore = 0;
    public Text bonusText;
    public int powerShots = 0;
    public Text powerShotsText;
    public int lives = 3;
    public Text livesText;

    //���������� ��� ��������
    public GameObject bulletPrefab;

    // ���������� ��� ������ �� ���������
    public int currentLevel = 0;

    // ���������� ��� ������ �� ����
    private int meatWinCondition = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        // �������� ������ � ���� �� ��������� ������
        livesText = GameObject.Find("LivesLeft").GetComponent<Text>();
        // �������� ������ � ���� �� ��������� ����������
        powerShotsText = GameObject.Find("PowerShots").GetComponent<Text>();
        // �������� ������ � ���� �� ��������� �����
        bonusText = GameObject.Find("BonusScore").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // ���� ����� ������ � ����� �� �����, �� �������
        if ( Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // ���� ����� ������ � ���������� ���������� ������ 0, �� �������� ���� �������� � ��������� ������
        if (Input.GetKeyDown(KeyCode.LeftControl) && powerShots > 0 && !gameOver)
        {
            powerShots--;
            playerAudio.PlayOneShot(powerShotSound, 1.0f);
            Instantiate(bulletPrefab, transform.position + Vector3.up * 3.5f, bulletPrefab.transform.rotation);
        }

        // ���������� �� ������ ���������� ������
        livesText.text = "Lives: " + lives.ToString();

        // ���������� �� ������ ���������� �����
        bonusText.text = "Meat collected: " + bonusScore.ToString();

        // ���������� �� ������ ���������� ����������
        powerShotsText.text = "Power Shots: " + powerShots.ToString();


    }

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
                gameOver = true; // ������������� ���� ���������� ����
                SceneManager.LoadScene("LevelTwoWin"); // ������� �� ����� � �������� �������

            }
            
        }

        //Start distance win
        if (collision.gameObject.CompareTag("DistanceWinTrigger"))
        {
            currentLevel = 1;
            Destroy(collision.gameObject);
        }


        //Obstacle collision
        //isOnGround = true;
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
            
            lives--;

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

                Invoke("GameOverTransition", 5f);
            }

            

            

        }
    }

}

