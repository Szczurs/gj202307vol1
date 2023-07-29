using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab to be spawned
    public int maxEnemiesPerSpawner = 5; // Maximum number of enemies to keep at a time for each spawner
    public float spawnInterval = 3f; // Time interval between enemy spawns
    public float spawnRange = 5f; // Range within which enemies can be spawned
    public string spawnerTag = "EnemySpawner"; // Tag to identify enemy spawners

    private List<GameObject> enemySpawners = new List<GameObject>(); // List to store references to all enemy spawners
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List to store references to all spawned enemies

    private void Start()
    {
        // Find all enemy spawners by tag and store their references
        FindEnemySpawners();

        // Start the enemy spawning coroutine
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void FindEnemySpawners()
    {
        // Find all GameObjects with the specified tag
        GameObject[] spawners = GameObject.FindGameObjectsWithTag(spawnerTag);

        // Loop through each spawner and add it to the list
        foreach (GameObject spawner in spawners)
        {
            enemySpawners.Add(spawner);
        }
    }

    private void SpawnEnemy(GameObject spawner)
    {
        // Calculate a random position within the specified range around the spawner's position
        Vector2 randomPosition = (Vector2)spawner.transform.position + Random.insideUnitCircle * spawnRange;

        // Instantiate a new enemy at the random position
        GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        spawnedEnemies.Add(newEnemy); // Add the new enemy to the list
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            // Loop through each spawner and spawn enemies if needed
            foreach (GameObject spawner in enemySpawners)
            {
                if (spawnedEnemies.Count < maxEnemiesPerSpawner)
                {
                    // Spawn a new enemy
                    SpawnEnemy(spawner);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
