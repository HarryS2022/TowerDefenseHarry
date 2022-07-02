using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    public Transform spawnPointLeft, spawnPointRight;
    public GameObject greenEnemyPrefab;
    public GameObject purpleEnemyPrefab;
    private List<GameObject> enemyPrefabs = new List<GameObject>();

    public float greenSpawnRate = 5;
    public float purpleSpawnRate = 5;
    public int enemyLimit = 5;
    public int greenCount = 5;
    public int purpleCount = 5;

    private float lastGreenSpawn = float.MinValue;
    private float lastPurpleSpawn = float.MinValue;
    private List<GameObject> enemiesSpawned = new List<GameObject>();
    private int enemyCount
    {
        get
        {
            int count = 0;
            foreach(GameObject enemy in enemiesSpawned)
            {
                if (enemy != null) count += 1;
            }
            return count;
        }
    }

    public int remainingEnemies
    {
        get
        {
            return enemyCount + greenCount + purpleCount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPurpleSpawn = Time.time + 10;
        enemyPrefabs.Add(greenEnemyPrefab);
        enemyPrefabs.Add(purpleEnemyPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time > lastGreenSpawn + greenSpawnRate && greenCount > 0 || Time.time > lastPurpleSpawn + purpleSpawnRate && purpleCount > 0) && enemyCount < enemyLimit)
        {
            int enemyCount = enemyLimit - this.enemyCount;
            int enemyID = Random.Range(0, enemyPrefabs.Count);
            int spawnPointID = Random.Range(0, 2);
            Transform spawnPoint = spawnPointID == 0 ? spawnPointLeft : spawnPointRight;
            if(enemyPrefabs[enemyID] == purpleEnemyPrefab)
            {
                if(lastPurpleSpawn + purpleSpawnRate < Time.time && purpleCount > 0)
                {
                    SpawnEnemy(enemyPrefabs[enemyID], spawnPoint);
                    lastPurpleSpawn = Time.time;
                    purpleCount -= 1;
                }
                


            }else if (enemyPrefabs[enemyID] == greenEnemyPrefab)
            {
                if (lastGreenSpawn + greenSpawnRate < Time.time && greenCount > 0)
                {
                    SpawnEnemy(enemyPrefabs[enemyID], spawnPoint);
                    lastGreenSpawn = Time.time;
                    greenCount -= 1;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, Transform spawnpoint)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnpoint.position;
        enemiesSpawned.Add(enemy);
    }
}
