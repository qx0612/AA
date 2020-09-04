using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject ShopUI;         //shop ui game object
    public GameObject turretSpot;

    public GameObject StartUI;        //start ui game object
    public GameObject TutorialUI;     //tutorial ui game object
    public GameObject tutorial;       //tutorial content game object
    public GameObject deathUI;        //death ui game object
    public GameObject winUI;          //win ui game object

    private void OnMouseUp()
    {
        if (!StartUI.activeInHierarchy && !deathUI.activeInHierarchy && !winUI.activeInHierarchy || !TutorialUI.activeInHierarchy) //if any of the UIs are not open
        {
            if (!ShopUI.activeInHierarchy)                  //if shop ui is not open
            {
                if (!tutorial.activeInHierarchy)            //if tutorial content is not open
                {
                    ShopUI.SetActive(true);                //open shop ui
                    ShopUI.GetComponent<Shop>().instance = turretSpot;    //set the shop instance to the turretspot that it was clicked on
                }
            }
        }
    }
}
