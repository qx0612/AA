using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turretPrefab;                                          //turret prefab 
    private GameObject turret;                                               //instantiated turret
    private GameManagerBehavior gameManager;                                 
    public bool occupied = false;                                            //bool to check if spot is occupied or not

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    private bool CanPlaceTurret()                                            //bool function to check if turret can be placed
    {
        int cost = turretPrefab.GetComponent<TurretData>().levels[0].cost;  //set the cost to the turrets current level cost
        return turret == null && gameManager.Gold >= cost;                   //returns if the gold is greater than cost, meaning player can buy
    }

    //1
    public void PlaceTurret()                                                //function to place turret
    {
        //2
        if (CanPlaceTurret())                                                //if you can place turret
        { 
            //3
            occupied = true;                                                 //sets the occupied bool to true
            turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);   //turret is set to the instantiated turret prefab   
            //4
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();              
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= turret.GetComponent<TurretData>().CurrentLevel.cost;      //the gold then minus the cost 
        }
    }

    public void UpgradeTurret()                                               //function to upgrade the turret      
    {
        if (CanUpgradeTurret())                                               //if turret can be upgraded 
        {
            turret.GetComponent<TurretData>().increaseLevel();                //call increaseLevel function
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= turret.GetComponent<TurretData>().CurrentLevel.cost;  //the gold the minus the cost
        }
    }

    private bool CanUpgradeTurret()                                           //bool functino to check if turret can be upgraded
    {
        if (turret != null)                                                   //if turret is not null
        {
            TurretData turretData = turret.GetComponent<TurretData>();        //get component to the turretdata
            TurretLevel nextLevel = turretData.getNextLevel();                //get reference to the getNextLevel function
            if (nextLevel != null)                                            //if next level does not return null
            {
                return gameManager.Gold >= nextLevel.cost;                    //return gold is greater than next level cost
            }
        }
        return false;            
    }

    public void SellTurret()                                                  //function to sell the turret
    {
        if (turret)                                                           //if there is a turret
        {
            gameManager.Gold += turret.GetComponent<TurretData>().CurrentLevel.cost/2;       //increases the gold from selling the turret
            turretPrefab = null;   //sets the turretprefab to null
            occupied = false;      //sets occupied to false
            Destroy(turret);       //destroy the turret
        }
    }
}
