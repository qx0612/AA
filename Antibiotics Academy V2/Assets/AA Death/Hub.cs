using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hub : MonoBehaviour
{
    public void BackToHub()
    {
        GameManager.sceneCounter = 0;
        GameManager.receptionistStage = 0;
        GameManager.pharmacistStage = 0;
        GameManager.doctorStage = 0;
        GameManager.surgeonStage = 0;

        SceneManager.LoadScene(7);
    }

}
