using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float timeReductionRate = 0.01f;

    // Новые переменные для управления временем спавна
    private float minSpawnTime = 2.0f;
    private float maxSpawnTime = 4.0f;
    private int spawnedMinesCount = 0;
    private const int MINES_THRESHOLD = 2; // Порог для начала уменьшения времени

    // Переменные для спавна бонусов в виде мяса
    public GameObject meatPrefab;
    private Vector3 meatSpawnPos = new Vector3(25, 11, 0);
    private float meatSpawnDelay = 5.0f;
    private float meatSpawnInterval = 10.0f;



    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnObstacle();
        SpawnBonus();

    }

    void SpawnBonus() {
        if (playerControllerScript.gameOver == false)
        {
            // Создаем бонус
            Instantiate(meatPrefab, meatSpawnPos, meatPrefab.transform.rotation);
            // Получаем случайное время для следующего спавна
            float nextSpawnTime = Random.Range(meatSpawnDelay, meatSpawnInterval);
            // Рекурсивно вызываем следующий спавн
            Invoke("SpawnBonus", nextSpawnTime);
        }
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Создаем препятствие
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            spawnedMinesCount++;

            // Применяем изменения времени только после достижения порога в 10 мин
            if (spawnedMinesCount > MINES_THRESHOLD)
            {
                // Уменьшаем минимальное время до предела 0.5
                minSpawnTime = Mathf.Max(0.5f, minSpawnTime - timeReductionRate);

                // Уменьшаем максимальное время до предела 1.5
                float reduction = timeReductionRate * (spawnedMinesCount - MINES_THRESHOLD);
                maxSpawnTime = Mathf.Max(1.5f, maxSpawnTime - reduction);

                // Убеждаемся, что maxSpawnTime не стало меньше minSpawnTime
                maxSpawnTime = Mathf.Max(maxSpawnTime, minSpawnTime);
            }

            // Получаем случайное время для следующего спавна
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            // Рекурсивно вызываем следующий спавн
            Invoke("SpawnObstacle", nextSpawnTime);
        }
    }
}