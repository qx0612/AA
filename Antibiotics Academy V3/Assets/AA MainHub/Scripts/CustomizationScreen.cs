using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationScreen : MonoBehaviour
{
    public bool IsGenderSelected; // bool to check if player has selected any character

    public GameObject AdvisePopUp; // warning message that pops up if player clicks ok without selecting a character

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMale() // function that triggers when player clicks on the male character in the character selection screen
    {
        PlayerCharacterCustomization.IsMale = true; // set bool IsMale in the PlayerCharacterCustomization script to be true
        IsGenderSelected = true; // set bool to true since player has selected a character
    }

    public void SelectFemale() // function that triggers when player clicks on the female character in the character selection screen
    {
        PlayerCharacterCustomization.IsMale = false; // set bool IsMale in the PlayerCharacterCustomization script to be false
        IsGenderSelected = true; // set bool to true since player has selected a character
    }

    public void GoToMainScene() // function to go to main scene after selecting a character
    {
        if (IsGenderSelected == true) // if player has selected a character
        {
            SceneManager.LoadScene("Main"); // go to main scene
        }
        else // if player has not selected a character
        {
            StartCoroutine(AdvisePopUpTime()); // warning message appear
        }
    }

    IEnumerator AdvisePopUpTime() // Coroutine function to let the warning text hover for a while before disappearing
    {
        AdvisePopUp.SetActive(true); // set the warning text to appear
        yield return new WaitForSeconds(2); // wait for 2 seconds 
        AdvisePopUp.SetActive(false); // make the warning text disappear
    }
}
