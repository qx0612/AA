using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class GameOver : MonoBehaviour
    {
        void RestartLevel()                                                 //function to restart level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //load the active scene which is the Tower defense
        }

    }

}