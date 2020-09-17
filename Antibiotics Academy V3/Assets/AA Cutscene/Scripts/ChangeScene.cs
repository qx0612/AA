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
        SceneManager.LoadScene(3); // Change to second cutscene
    }

    public void CutScene3()
    {
        SceneManager.LoadScene(4); // Change to third cutscene
    }

    public void CutScene4()
    {
        SceneManager.LoadScene(5); // Change to fourth cutscene
    }

    public void CharacterSelectionScene()
    {
        SceneManager.LoadScene(6); // Change to character selection scene
    }
}
