using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    public float distanceTravelled = 0; // ���������� ��� �������� ����������� ����������
    public Text distanceText; // ������ �� UI-������� ��� ����������� ����������
    private MoveLeft moveLeftScript; // ������ �� ������ MoveLeft
    private PlayerController playerControllerScript;


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
        }

        // ��������� ����� �� ������
        distanceText.text = "Distance: " + Mathf.Floor(distanceTravelled).ToString() + "m";
    }
}