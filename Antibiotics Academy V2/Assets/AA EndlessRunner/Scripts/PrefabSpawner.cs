using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private float nextSpawn = 0;
    public GameObject[] prefabToSpawn;
    public float spawnRate = 1.25f;
    public float randomDelay = 1;
    public Transform spawnPoint;

    public bool prevObj3;

    public GameObject player;
    PlayerController playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<PlayerController>();
        prevObj3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            if (playercontroller.yourScore > 0 && playercontroller.yourScore <= 20) //speed 8
            {
                Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity);
                nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
            }

            else if (playercontroller.yourScore > 20 && playercontroller.yourScore <= 75) // speed 10
            {
                int randomObstacle = Random.Range(0, 2);

                if (randomObstacle == 0 && Time.time > nextSpawn)
                {
                    Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity);
                    nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                }
                else if (randomObstacle == 1 && Time.time > nextSpawn)
                {
                    Instantiate(prefabToSpawn[1], spawnPoint.position, Quaternion.identity);
                    nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                }
            }

            else if (playercontroller.yourScore > 75) // speed 12
            {
                if (!prevObj3)
                {
                    int randomObstacle = Random.Range(0, 3);

                    if (randomObstacle == 0 && Time.time > nextSpawn)
                    {
                        Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity);
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                    }
                    else if (randomObstacle == 1 && Time.time > nextSpawn)
                    {
                        Instantiate(prefabToSpawn[1], spawnPoint.position, Quaternion.identity);
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                    }
                    else if (randomObstacle == 2 && Time.time > nextSpawn)
                    {
                        Instantiate(prefabToSpawn[2], spawnPoint.position, Quaternion.identity);
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                        prevObj3 = true;
                    }

                }
                else
                {
                    int randomObstacle = Random.Range(0, 2);

                    if (randomObstacle == 0 && Time.time > nextSpawn)
                    {
                        Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity);
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                        prevObj3 = false;
                    }
                    else if (randomObstacle == 1 && Time.time > nextSpawn)
                    {
                        Instantiate(prefabToSpawn[2], spawnPoint.position, Quaternion.identity);
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
                    }
                }

            }

        }
    }
}
