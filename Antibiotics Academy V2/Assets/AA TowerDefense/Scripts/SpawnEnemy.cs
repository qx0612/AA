using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Wave
{
    public List<GameObject> enemyPrefabs;
    public List<int> enemyCount;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    private GameObject newEnemy;

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            // 20
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                // 3  
                lastSpawnTime = Time.time;
                //int index = Random.Range(0, waves[currentWave].enemyPrefabs.Length);
                //GameObject newEnemy = Instantiate(waves[currentWave].enemyPrefabs[index]);
                CheckInstantiate();
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // 4 
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // 5 
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }

    void CheckInstantiate()
    {
        int index = Random.Range(0, waves[gameManager.Wave].enemyPrefabs.Count);
        //Debug.Log(index);

        if (waves[gameManager.Wave].enemyCount[index] > 0)
        {
            InstantiateEnemy(index);
        }
        else if (waves[gameManager.Wave].enemyCount[index] == 0)
        {
            waves[gameManager.Wave].enemyCount.RemoveAt(index);
            waves[gameManager.Wave].enemyPrefabs.RemoveAt(index);
            int newIndex = Random.Range(0, waves[gameManager.Wave].enemyPrefabs.Count);
            InstantiateEnemy(newIndex);
        }
    }

    void InstantiateEnemy(int index)
    {
        newEnemy = Instantiate(waves[gameManager.Wave].enemyPrefabs[index]);
        waves[gameManager.Wave].enemyCount[index] -= 1;
        if (waves[gameManager.Wave].enemyCount[index] == 0)
        {
            waves[gameManager.Wave].enemyCount.RemoveAt(index);
            waves[gameManager.Wave].enemyPrefabs.RemoveAt(index);
        }
    }
}
