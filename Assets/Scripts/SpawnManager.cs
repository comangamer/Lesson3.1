using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float timeReductionRate = 0.01f;

    // Ќовые переменные дл€ управлени€ временем спавна
    private float minSpawnTime = 2.0f;
    private float maxSpawnTime = 4.0f;
    private int spawnedMinesCount = 0;
    private const int MINES_THRESHOLD = 2; // ѕорог дл€ начала уменьшени€ времени

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
            // —оздаем преп€тствие
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            spawnedMinesCount++;

            // ѕримен€ем изменени€ времени только после достижени€ порога в 10 мин
            if (spawnedMinesCount > MINES_THRESHOLD)
            {
                // ”меньшаем минимальное врем€ до предела 0.5
                minSpawnTime = Mathf.Max(0.5f, minSpawnTime - timeReductionRate);

                // ”меньшаем максимальное врем€ до предела 1.5
                float reduction = timeReductionRate * (spawnedMinesCount - MINES_THRESHOLD);
                maxSpawnTime = Mathf.Max(1.5f, maxSpawnTime - reduction);

                // ”беждаемс€, что maxSpawnTime не стало меньше minSpawnTime
                maxSpawnTime = Mathf.Max(maxSpawnTime, minSpawnTime);
            }

            // ѕолучаем случайное врем€ дл€ следующего спавна
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            // –екурсивно вызываем следующий спавн
            Invoke("SpawnObstacle", nextSpawnTime);
        }
    }
}