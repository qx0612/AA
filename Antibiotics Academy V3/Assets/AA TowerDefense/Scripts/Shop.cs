using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject instance;                //the instance of the turretSpot
    public Turret turretInstance;              //the instance of the turret
    public GameObject antibioticTurret;        //antibiotic turret
    public GameObject waterTurret;             //water turret  
    public GameObject fruitTurret;             //fruit turret


    public void AntibioticTurret()                         //function to buy / upgrade antibiotic turret
    {
        turretInstance = instance.GetComponent<Turret>();  //turret instance is set to the turret component of the turretspot 

        if (instance != null)                  //if the turretSpot isnt null
        {
            if (!turretInstance.occupied)      //if the turretSpot isnt occupied
            {
                turretInstance.turretPrefab = antibioticTurret;   //set the turretPrefab in the turret instance to antibiotic turret
                turretInstance.PlaceTurret();                     //place turret
            }
            else if (turretInstance.turretPrefab.CompareTag("AntibioticTurr"))  //if there is an antibiotic turret
            {
                turretInstance.UpgradeTurret();                    //upgrade the turret
            }
        }
    }

    public void WaterTurret()                              //function to buy / upgrade water turret  
    {
        turretInstance = instance.GetComponent<Turret>();  //turret instance is set to the turret component of the turretspot

        if (instance != null)                  //if the turretSpot isnt null         
        {
            if (!turretInstance.occupied)      //if the turretSpot isnt occupied
            {
                turretInstance.turretPrefab = waterTurret;       //set the turretPrefab in the turret instance to antibiotic turret
                turretInstance.PlaceTurret();                    //place turret
            }
            else if (turretInstance.turretPrefab.CompareTag("WaterTurr"))   //if there is a water turret
            { 
                turretInstance.UpgradeTurret();                    //upgrade the turret 
            }
        }
    }

    public void FruitTurret()                               //function to buy / upgrade fruit turret
    {
        turretInstance = instance.GetComponent<Turret>();   //turret instance is set to the turret component of the turretspot

        if (instance != null)                  //if the turretSpot isnt null 
        {
            if (!turretInstance.occupied)       //if the turretSpot isnt occupied
            {
                turretInstance.turretPrefab = fruitTurret;     //set the turretPrefab in the turret instance to antibiotic turret    
                turretInstance.PlaceTurret();                  //place turret
            }
            else if (turretInstance.turretPrefab.CompareTag("FruitTurr"))  //if there is a fruit turret
            {
                turretInstance.UpgradeTurret();   //upgrade the turret 
            }
        }
    }

    public void SellTurret()                                 //function to sell the turret
    {
        turretInstance = instance.GetComponent<Turret>();    //turret instance is set to the turret component of the turretspot
        turretInstance.SellTurret();                         //call sell turret
    }
}
