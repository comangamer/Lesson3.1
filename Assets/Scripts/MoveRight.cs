using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 10f; // �������� �������� �������
    private PlayerController playerControllerScript; // ������ �� ������ PlayerController
    private float rightBound = 14; // ������ �� ��� X, �� ������� ������ ����� ���������
    public AudioClip powerShotCollisionSound; // ���� ��� ������������ � ������������


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ������� ������ � ������ "Player" � �������� ��������� PlayerController
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // ������� ������ ������
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ���������� �� ������ � ����� "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���������� ������ ��� ������������
            Destroy(gameObject);
            // ���������� ������, � ������� ��������� ������������
            Destroy(collision.gameObject);
            // ����������� ���� �����������

        }

        if (collision.gameObject.CompareTag("Destroyer"))
        {
            // ���������� ������ ��� ������������
            Destroy(gameObject);
        }

    }
}
