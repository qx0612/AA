using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
// This script is attached to the destroyer game object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bg"))
        {
            // Do nothing if destroyer game object collides with game object with tag "bg"
        }
        if (other.gameObject.CompareTag("grass"))
        {
            // Do nothing if destroyer game object collides with game object with tag "grass"
        }
        else
        {
            Destroy(other.gameObject); // Destroys any other game object that the destroyer game object collides with
        }
    }
}
