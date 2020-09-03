using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turretPrefab;
    private GameObject turret;
    private GameManagerBehavior gameManager;
    public bool occupied = false;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CanPlaceTurret()
    {
        int cost = turretPrefab.GetComponent<MonsterData>().levels[0].cost;
        return turret == null && gameManager.Gold >= cost;
    }

    //1
    public void PlaceTurret()
    {
        //2
        if (CanPlaceTurret())
        {
            //3
            occupied = true;
            turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
            //4
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= turret.GetComponent<MonsterData>().CurrentLevel.cost;
        }
        //else if (CanUpgradeTurret())
        //{
        //    turret.GetComponent<MonsterData>().increaseLevel();
        //    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //    audioSource.PlayOneShot(audioSource.clip);

        //    gameManager.Gold -= turret.GetComponent<MonsterData>().CurrentLevel.cost;
        //}
    }

    public void UpgradeTurret()
    {
        if (CanUpgradeTurret())
        {
            turret.GetComponent<MonsterData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= turret.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeTurret()
    {
        if (turret != null)
        {
            MonsterData monsterData = turret.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    public void SellTurret()
    {
        if (turret)
        {
            gameManager.Gold += turret.GetComponent<MonsterData>().CurrentLevel.cost/2;
            turretPrefab = null;
            occupied = false;
            Destroy(turret);
        }
    }
}
