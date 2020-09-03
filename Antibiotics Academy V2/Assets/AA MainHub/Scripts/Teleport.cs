using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject A; // position 

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = A.transform.position;
    }
}
