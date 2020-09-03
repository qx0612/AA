using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutScene2()
    {
        SceneManager.LoadScene(3);
    }

    public void CutScene3()
    {
        SceneManager.LoadScene(4);
    }

    public void CutScene4()
    {
        SceneManager.LoadScene(5);
    }

    public void CharacterSelectionScene()
    {
        SceneManager.LoadScene(6);
    }
}
