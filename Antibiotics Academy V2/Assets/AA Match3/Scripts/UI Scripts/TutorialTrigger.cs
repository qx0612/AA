using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class TutorialTrigger : MonoBehaviour
    {
        public Tutorial tutorial;
        public GameObject StartUI;

        public void TriggerTutorial()
        {
            FindObjectOfType<TutorialManager>().StartTutorial(tutorial);
            StartUI.SetActive(false);
        }
    }
}