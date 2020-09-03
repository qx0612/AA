using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class EndGameUI : MonoBehaviour
    {
        public void TriggerRestart()  //function to restart the game
        {
            SceneManager.LoadScene(8); // match 3
        }

        public void TriggerQuit() //function to quit the game
        {
            GameManager.pharmacistStage = 2;
            GameManager.receptionistStage = 2;

            SceneManager.LoadScene(7); //main

        }

        public void TriggerQuitLost()  //function to quit the game when player lost
        {
            SceneManager.LoadScene(10); //death
        }
    }
}
