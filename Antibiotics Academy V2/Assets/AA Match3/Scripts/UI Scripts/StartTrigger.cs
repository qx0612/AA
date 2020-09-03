using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class StartTrigger : MonoBehaviour
    {
        public GameObject StartUI;

        public void TriggerStart()
        {
            Time.timeScale = 1f;
            StartUI.SetActive(false);
        }
    }
}
