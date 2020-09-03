using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed; // set speed of obstacle's movement

    public GameObject player; // get the player game object
    PlayerController playercontroller; // get the player controller script

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f; // set obstacle movement speed to 8
        player = GameObject.FindGameObjectWithTag("Player"); // find the player game object with tag "Player"
        playercontroller = player.GetComponent<PlayerController>(); // get the player controller component from the player game object
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.yourScore > 50 && playercontroller.yourScore <= 75) // if score is more than 50 and less than or equals to 75
        {
            speed = 10f; // set obstacle movement speed to 10
        }
        else if (playercontroller.yourScore > 75) // if score is more than 75
        {
            speed = 12f; // set obstacle movement speed to 12
        }

        transform.position += Vector3.left * speed * Time.deltaTime; // update the position of the obstacles based on the speed and time in the left direction

    }
}
