using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class StartTrigger : MonoBehaviour
    {
        public GameObject StartUI;

        public void TriggerStart()    //function to start the game
        {
            Time.timeScale = 1f;
            StartUI.SetActive(false);
        }
    }
}
