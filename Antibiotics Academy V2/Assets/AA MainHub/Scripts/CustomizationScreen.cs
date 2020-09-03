using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationScreen : MonoBehaviour
{
    public bool IsGenderSelected;

    public GameObject AdvisePopUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMale()
    {
        PlayerCharacterCustomization.IsMale = true;
        IsGenderSelected = true;
        Debug.Log("Male");
    }

    public void SelectFemale()
    {
        PlayerCharacterCustomization.IsMale = false;
        IsGenderSelected = true;
        Debug.Log("Female");
    }

    public void GoToMainScene()
    {
        if (IsGenderSelected == true)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            StartCoroutine(AdvisePopUpTime());
        }
    }

    IEnumerator AdvisePopUpTime()   // Let the text hover for a while before disappearing
    {
        AdvisePopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        AdvisePopUp.SetActive(false);
    }
}
