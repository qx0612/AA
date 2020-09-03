using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bg"))
        {

        }
        if (other.gameObject.CompareTag("grass"))
        {

        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
