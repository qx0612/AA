using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject turretSpot;

    public GameObject StartUI;
    public GameObject TutorialUI;
    public GameObject tutorial;
    public GameObject deathUI;
    public GameObject winUI;

    private void OnMouseUp()
    {
        if (!StartUI.activeInHierarchy && !deathUI.activeInHierarchy && !winUI.activeInHierarchy || !TutorialUI.activeInHierarchy)
        {
            if (!ShopUI.activeInHierarchy)
            {
                if (!tutorial.activeInHierarchy)
                {
                    ShopUI.SetActive(true);
                    ShopUI.GetComponent<Shop>().instance = turretSpot;
                }
            }
        }
    }
}
