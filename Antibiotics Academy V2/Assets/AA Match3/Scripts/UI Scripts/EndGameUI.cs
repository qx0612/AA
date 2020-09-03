using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class EndGameUI : MonoBehaviour
    {
        //public bool switchedScene = false;

        //private int sceneTocontinue;


        public void Start()
        {

        }

        public void TriggerRestart()
        {
            SceneManager.LoadScene(8); // match 3
        }

        public void TriggerQuit() // win
        {
            GameManager.pharmacistStage = 2;
            GameManager.receptionistStage = 2;

            SceneManager.LoadScene(7); //main

            //sceneTocontinue = PlayerPrefs.GetInt("savedScene"); // main scene

            //if (sceneTocontinue != 0)
            //{
            //    //switchedScene = true;
            //    SceneManager.LoadScene(sceneTocontinue);
            //}
            //else
            //    return;
        }

        public void TriggerQuitLost()
        {
            SceneManager.LoadScene(10); //death
        }
        //private void Awake()
        //{
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}

        //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    PlayerPrefs.SetString("2", scene.name);
        //}

        //public static void LoadLastScene()
        //{
        //    SceneManager.LoadScene(PlayerPrefs.GetString("1"));
        //}
    }
}
