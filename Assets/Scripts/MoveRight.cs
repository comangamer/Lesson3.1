using UnityEngine;

/*
This script is responsible for moving the object to the right
The objects we want to move right is out bullets
Our bullets are metalDetectors (don't ask)
The issue I got here is wrong pivots in it's 3d model
So basically we are moving em UP... Well...
 
 */


public class MoveRight : MonoBehaviour
{
    public float speed = 10f;                           // Object velocity
    private PlayerController playerControllerScript;    // Link to PlayerController script
    private float rightBound = 14;                      // X-axis limit beyond which the object will be destroyed
    public AudioClip powerShotCollisionSound;           // Sound when hitting an obstacle


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the object named “Player” and get the PlayerController component
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Move the object to the right (yeah, up is right here)
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has encountered the “Obstacle” tag
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the object on impact
            Destroy(gameObject);
            // Destroy the colliding object
            Destroy(collision.gameObject);
            // Playing the sound of annihilation

        }

        if (collision.gameObject.CompareTag("Destroyer"))
        {
            // Destroy the object on impact
            Destroy(gameObject);
        }

    }
}
