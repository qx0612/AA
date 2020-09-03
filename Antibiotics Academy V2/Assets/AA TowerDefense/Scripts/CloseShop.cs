using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject ShopUI;

    public void ExitShop()
    {
        //Time.timeScale = 1f;
        ShopUI.GetComponent<Shop>().instance = null;
        ShopUI.GetComponent<Shop>().turretInstance = null;

        ShopUI.SetActive(false);
    }
}
