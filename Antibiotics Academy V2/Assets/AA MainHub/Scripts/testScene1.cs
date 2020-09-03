using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene1 : MonoBehaviour
{

    GameObject player;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Load()
    {
        SceneManager.LoadScene(8); // match 3
    }
}
