using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TutorialTrigger1 : MonoBehaviour
    {
        public Tutorial1 tutorial;
        public GameObject StartUI;

        public void TriggerTutorial()
        {
            FindObjectOfType<TutorialManager1>().StartTutorial(tutorial);
            StartUI.SetActive(false);
        }
    }
}