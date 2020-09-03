using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene : MonoBehaviour
{
    public static void LoadLastScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Main"));
    }
}
