using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float timeReductionRate = 0.01f;

    // ����� ���������� ��� ���������� �������� ������
    private float minSpawnTime = 2.0f;
    private float maxSpawnTime = 4.0f;
    private int spawnedMinesCount = 0;
    private const int MINES_THRESHOLD = 2; // ����� ��� ������ ���������� �������

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            // ������� �����������
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            spawnedMinesCount++;

            // ��������� ��������� ������� ������ ����� ���������� ������ � 10 ���
            if (spawnedMinesCount > MINES_THRESHOLD)
            {
                // ��������� ����������� ����� �� ������� 0.5
                minSpawnTime = Mathf.Max(0.5f, minSpawnTime - timeReductionRate);

                // ��������� ������������ ����� �� ������� 1.5
                float reduction = timeReductionRate * (spawnedMinesCount - MINES_THRESHOLD);
                maxSpawnTime = Mathf.Max(1.5f, maxSpawnTime - reduction);

                // ����������, ��� maxSpawnTime �� ����� ������ minSpawnTime
                maxSpawnTime = Mathf.Max(maxSpawnTime, minSpawnTime);
            }

            // �������� ��������� ����� ��� ���������� ������
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            // ���������� �������� ��������� �����
            Invoke("SpawnObstacle", nextSpawnTime);
        }
    }
}