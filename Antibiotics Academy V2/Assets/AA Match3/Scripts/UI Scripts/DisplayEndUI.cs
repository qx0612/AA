using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class DisplayEndUI : MonoBehaviour
    {
        public GameObject DeathUI;
        public GameObject WinUI;

        public void DisplayDeathUI()
        {
            DeathUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void DisplayWinUI()
        {
            WinUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}