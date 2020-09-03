using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;

    public GameObject player;
    PlayerController playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.yourScore > 50 && playercontroller.yourScore <= 75)
        {
            speed = 10f;
        }
        else if (playercontroller.yourScore > 75)
        {
            speed = 12f;
        }

        transform.position += Vector3.left * speed * Time.deltaTime;

    }
}
