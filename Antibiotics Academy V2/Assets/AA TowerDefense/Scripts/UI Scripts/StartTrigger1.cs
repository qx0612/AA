using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class StartTrigger1 : MonoBehaviour
    {
        public GameObject StartUI;

        public GameObject heart;
        private AudioSource src;

        public void TriggerStart()
        {
            Time.timeScale = 1f;
            src = heart.GetComponent<AudioSource>();
            StartUI.SetActive(false);

            if (!src.isPlaying)
            {
                src.Play();
            }
        }
    }
}
