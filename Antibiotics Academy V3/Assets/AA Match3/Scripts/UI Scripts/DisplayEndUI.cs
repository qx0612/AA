using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class DisplayEndUI : MonoBehaviour
    {
        public GameObject DeathUI;     //store reference to the death ui
        public GameObject WinUI;       //store reference to the win ui 

        public void DisplayDeathUI()   //function to display the death ui
        {
            DeathUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void DisplayWinUI()     //function to display the win ui
        {
            WinUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}