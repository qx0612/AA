using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class EndGameUI1 : MonoBehaviour
    {
        public void TriggerRestart()
        {
            SceneManager.LoadScene(9); // restart tower defense game
        }

        public void TriggerQuit() // win tower defense
        {
            // back to hub
            GameManager.surgeonStage = 3;

            SceneManager.LoadScene(7); // go back to hospital
        }

        public void TriggerQuitLost()
        {
            SceneManager.LoadScene(7); // back to main
            GameManager.surgeonStage = 0;
        }
    }
}
