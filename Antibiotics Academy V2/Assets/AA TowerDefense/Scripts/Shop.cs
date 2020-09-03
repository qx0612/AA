using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject instance; 
    public Turret turretInstance;
    public GameObject antibioticTurret;
    public GameObject waterTurret;
    public GameObject sleepTurret;


    public void AntibioticTurret()
    {
        turretInstance = instance.GetComponent<Turret>();

        if (instance != null)
        {
            if (!turretInstance.occupied)
            {
                turretInstance.turretPrefab = antibioticTurret;
                turretInstance.PlaceTurret();
            }
            else if (turretInstance.turretPrefab.CompareTag("AntibioticTurr"))
            {
                turretInstance.UpgradeTurret();
            }
        }
    }

    public void WaterTurret()
    {
        turretInstance = instance.GetComponent<Turret>();

        if (instance != null)
        {
            if (!turretInstance.occupied)
            {
                turretInstance.turretPrefab = waterTurret;
                turretInstance.PlaceTurret();
            }
            else if (turretInstance.turretPrefab.CompareTag("WaterTurr"))
            {
                turretInstance.UpgradeTurret();
            }
        }
    }

    public void SleepTurret()
    {
        turretInstance = instance.GetComponent<Turret>();

        if (instance != null)
        {
            if (!turretInstance.occupied)
            {
                turretInstance.turretPrefab = sleepTurret;
                turretInstance.PlaceTurret();
            }
            else if (turretInstance.turretPrefab.CompareTag("SleepTurr"))
            {
                turretInstance.UpgradeTurret();
            }
        }
    }

    public void SellTurret()
    {
        turretInstance = instance.GetComponent<Turret>();
        turretInstance.SellTurret();
    }
}
