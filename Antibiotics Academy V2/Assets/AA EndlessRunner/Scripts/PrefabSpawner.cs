using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private float nextSpawn = 0; // time interval between each obstacle spawn
    public GameObject[] prefabToSpawn; // array to store all the obstacles
    public float spawnRate = 1.25f; // set the spawn rate of the obstacles
    public float randomDelay = 1; // set a random delay to the spawn interval
    public Transform spawnPoint; // set the spawn position of the enemies

    public bool prevObj3; // bool to check if the previous obstacle spawned was obstacle 3

    public GameObject player; // player game object
    PlayerController playercontroller; // player controller component

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // get the player game object
        playercontroller = player.GetComponent<PlayerController>(); // get the player controller component
        prevObj3 = false; // set bool to false since no obstacles was spawned before start
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn) // if able to spawn obstacle
        {
            if (playercontroller.yourScore > 0 && playercontroller.yourScore <= 20) // if score is more than 0 but less than or equals to 20 (speed 8)
            {
                Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity); // instantiate element 0 of the prefabToSpawn array at the spawn position
                nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);  // set the time interval for the next spawn
            }

            else if (playercontroller.yourScore > 20 && playercontroller.yourScore <= 75) // if score is more than 20 but less than or equals to 75 (speed 10)
            {
                int randomObstacle = Random.Range(0, 2); // randomly choose an integer between 0 and 1

                if (randomObstacle == 0 && Time.time > nextSpawn) // if the integer randomly generated is 0
                {
                    Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity); // instantiate element 0 of the prefabToSpawn array at the spawn position
                    nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                }
                else if (randomObstacle == 1 && Time.time > nextSpawn) // if the integer randomly generated is 1
                {
                    Instantiate(prefabToSpawn[1], spawnPoint.position, Quaternion.identity); // instantiate element 1 of the prefabToSpawn array at the spawn position
                    nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);  // set the time interval for the next spawn
                }
            }

            else if (playercontroller.yourScore > 75) // if score is more than 75 (speed 12)
            {
                if (!prevObj3) // if previous object spawned is not element 2 in the prefabToSpawn array
                {
                    int randomObstacle = Random.Range(0, 3); // randomly generate an integer between 0 to 2

                    if (randomObstacle == 0 && Time.time > nextSpawn) // if the integer randomly generated is 0 
                    {
                        Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity); // instantiate element 0 of the prefabToSpawn array at the spawn position
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                    }
                    else if (randomObstacle == 1 && Time.time > nextSpawn) // if the integer randomly generated is 1
                    {
                        Instantiate(prefabToSpawn[1], spawnPoint.position, Quaternion.identity); // instantiate element 1 of the prefabToSpawn array at the spawn position
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                    }
                    else if (randomObstacle == 2 && Time.time > nextSpawn) // if the integer randomly generated is 2
                    {
                        Instantiate(prefabToSpawn[2], spawnPoint.position, Quaternion.identity); // instantiate element 2 of the prefabToSpawn array at the spawn position
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                        prevObj3 = true; // set bool to true since obstacle spawned is element 2
                    }

                }
                else
                {
                    int randomObstacle = Random.Range(0, 2); // randomly generate an integer between 0 to 1

                    if (randomObstacle == 0 && Time.time > nextSpawn) // if the integer randomly generated is 0 
                    {
                        Instantiate(prefabToSpawn[0], spawnPoint.position, Quaternion.identity); // instantiate element 0 of the prefabToSpawn array at the spawn position
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                        prevObj3 = false; // set bool to false since obstacle spawned is not element 2
                    }
                    else if (randomObstacle == 1 && Time.time > nextSpawn) // if the integer randomly generated is 1
                    {
                        Instantiate(prefabToSpawn[2], spawnPoint.position, Quaternion.identity); // instantiate element 2 of the prefabToSpawn array at the spawn position
                        nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay); // set the time interval for the next spawn
                    }
                }

            }

        }
    }
}
