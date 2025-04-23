using UnityEngine;
using UnityEngine.UI;

/*
Originally, the mechanics of successfully passed mines was the main game mechanic
But after I decided to add a distance counter, it became a legacy
 
 */


public class MinesController : MonoBehaviour
{




    public int minesPassed = 0; // Variable for storing the number of passed mines
    public Text minesText;      // Link to UI element for displaying the number of passed mines

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minesText = GameObject.Find("MinesPassed").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        minesText.text = "Mines Passed: " + minesPassed.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            minesPassed++;
            minesText.text = "Mines Passed: " + minesPassed.ToString();

        }
    }
}
