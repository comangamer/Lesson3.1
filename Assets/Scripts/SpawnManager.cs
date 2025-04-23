using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;                   // Obstacle Prefab
    private Vector3 spawnPos = new Vector3(25, 0, 0);   // Spawn position
    private float startDelay = 2;                       // Delay before the first spawn
    private float timeReductionRate = 0.01f;            // Rate of time reduction between spawns

    // New variables to control spawn time
    private float minSpawnTime = 2.0f;      // Minimum spawning time
    private float maxSpawnTime = 4.0f;      // Maximum spawning time 
    private int spawnedMinesCount = 0;      // Number of spawned mines
    private const int MINES_THRESHOLD = 2;  // Threshold for starting time reduction

    // Variables for spawning bonuses in the form of meat
    public GameObject meatPrefab;                           // Meat Prefab
    private Vector3 meatSpawnPos = new Vector3(25, 11, 0);  // Meat spawn position
    private float meatSpawnDelay = 5.0f;                    // Delay before first spawn
    private float meatSpawnInterval = 10.0f;                // Interval between spawns

    private PlayerController playerControllerScript;

    // Trying to make the spawn objects for levels different
    // From what I understood this is allows us to change the level in the inspector
    [SerializeField] // To be able to customize in the inspector
    private int currentLevel = 1; // Add a level variable


    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnObstacle();

        // Spawn bonuses only if the level is greater than 1 (what means 2 obviously)
        if (currentLevel > 1)
        {
            SpawnBonus();
        }

    }


    void SpawnBonus() {
        if (playerControllerScript.gameOver == false)
        {
            // Creating a bonus
            Instantiate(meatPrefab, meatSpawnPos, meatPrefab.transform.rotation);
            // We get a random time for the next spawn
            float nextSpawnTime = Random.Range(meatSpawnDelay, meatSpawnInterval);
            // Recursively invoke the following spawn
            Invoke("SpawnBonus", nextSpawnTime);
        }
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Create an obstacle
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            spawnedMinesCount++;

            // Apply time changes only after the threshold of MINES_THRESHOLD mines has been reached
            if (spawnedMinesCount > MINES_THRESHOLD)
            {
                // Reduce the minimum time to a limit of 0.5
                minSpawnTime = Mathf.Max(0.5f, minSpawnTime - timeReductionRate);

                // Reduce the maximum time to a limit of 1.5
                float reduction = timeReductionRate * (spawnedMinesCount - MINES_THRESHOLD);
                maxSpawnTime = Mathf.Max(1.5f, maxSpawnTime - reduction);

                // Make sure that maxSpawnTime is not less than minSpawnTime
                maxSpawnTime = Mathf.Max(maxSpawnTime, minSpawnTime);
            }

            // We get a random time for the next spavin
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            // Recursively invoke the following spavn
            Invoke("SpawnObstacle", nextSpawnTime);
        }
    }
}