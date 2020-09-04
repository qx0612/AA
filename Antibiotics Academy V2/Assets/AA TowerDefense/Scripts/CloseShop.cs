using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject ShopUI;

    public void ExitShop()            //function to exit sohp
    {
        //Time.timeScale = 1f;
        ShopUI.GetComponent<Shop>().instance = null;             //resets the shop instance and turretinstance to null
        ShopUI.GetComponent<Shop>().turretInstance = null;

        ShopUI.SetActive(false);                                //close the shop ui
    }
}
