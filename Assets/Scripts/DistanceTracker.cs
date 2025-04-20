using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    public float distanceTravelled = 0f; // ���������� ��� �������� ����������� ����������
    
    public Text distanceText; // ������ �� UI-������� ��� ����������� ����������
    private MoveLeft moveLeftScript; // ������ �� ������ MoveLeft
    private PlayerController playerControllerScript;

    private float targetDistance = 1001f; // ������� ���������� ��� ������



    // ����� Start ���������� ����� ������ ������
    void Start()
    {
        // ������� ������ � ������ "DistanceText" � ����� � �������� ��������� Text
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();

        // ������� ������ � ����������� MoveLeft (��������, ��� ��� �����������)
        moveLeftScript = GameObject.FindObjectOfType<MoveLeft>();

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        

    }

    // ����� Update ���������� ������ ����
    void Update()
    {

        if (playerControllerScript.gameOver == false)
        { 
            // ����������� ���������� ���������� �� ������ �������� � �������
            distanceTravelled += moveLeftScript.speed * Time.deltaTime / 3;

            // �������� ������� ������� �� ������� PlayerController
            int level = playerControllerScript.currentLevel;

            
            if (level == 1)
            {
                // ���������, ���������� �� ������� ���������� ��� ������ ����� ����� ������� 1
                if (distanceTravelled >= targetDistance)
                {
                    // ��������� ���� � ��������� �� �������� �����
                    GameOver();
                }
            }
            




        }

        // ��������� ����� �� ������
        distanceText.text = "Distance: " + Mathf.Floor(distanceTravelled).ToString() + "m";

        

    }

    void GameOver()
    {
        playerControllerScript.gameOver = true; // ������������� ���� ���������� ����
        SceneManager.LoadScene("LevelOneWin"); // ������� �� ����� � �������� �������
    }

}