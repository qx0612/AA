using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{

    public float speed = 10;
    public float damage;
    public GameObject target;   
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public string bulletTag;

    private float bacteriaPenalty;   //the damage penalty for bacteria
    private float virusPenalty;      //the damage penalty for virus
    private float bacteriaResPenalty;//the damage penalty for drug resistant bacteria

    private float distance;
    private float startTime;

    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();

        switch (bulletTag)
        {
            case "Antibiotic":            //if the bulletTag is antibiotic
                bacteriaPenalty = 0;      //0% penalty on bacteria
                virusPenalty = 100;       //100% penalty on virus (means no damage)
                bacteriaResPenalty = 80;  //80% penalty on drug resistant bacteria (20% damage)
                break;
            case "Water":                 //if the bulletTag is water
                bacteriaPenalty = 80;     //80% penalty on bacteria (20% damage)
                virusPenalty = 0;         //0% penalty on virus
                bacteriaResPenalty = 0;   //0% penalty on drug resistant bacteria
                break;
            case "Fruit":                 //if the bulletTag is fruit
                bacteriaPenalty = 0;      //0% penalty on bacteria  
                virusPenalty = 0;         //0% penalty on virus  
                bacteriaResPenalty = 0;   //0% penalty on drug resistant bacteria
                break;
            default:
                bacteriaPenalty = 0;
                virusPenalty = 0;
                bacteriaPenalty = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);  //updating the position of the bullet

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))  //if bullet reached target position
        {
            if (target != null)                                                                          //if target is not null
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");                       //get reference to target health bar
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                if (target.name == "EnemyBacteria(Clone)")                                               //if target is bacteria
                {
                    damage = damage - (damage * (bacteriaPenalty / 100));                                //calculate damage
                    Debug.Log(damage);
                    healthBar.currentHealth -= Mathf.Max(damage, 0);                                     //bacteria health minus off the damage
                }
                
                else if (target.name == "EnemyBacteriaRes(Clone)")                                       //if target is drug resistant bacteria
                {
                    damage = damage - (damage * (bacteriaResPenalty / 100));                             //calculate damage
                    Debug.Log(damage);

                    healthBar.currentHealth -= Mathf.Max(damage, 0);                                     //drug resistant bacteria health minus off the damage
                }

                else if (target.name == "EnemyVirus(Clone)")                                             //if target is virus
                {
                    damage = damage - (damage * (virusPenalty / 100));                                   //calculate damage
                    Debug.Log(damage);

                    healthBar.currentHealth -= Mathf.Max(damage, 0);                                     //virus health minus off the damage
                }

                if (healthBar.currentHealth <= 0)                                                        //if health reaches 0 or below
                {
                    Destroy(target);                                                                     //destroy target
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    gameManager.Gold += 30;                                                              //increase gold by 30
                }
            }
            Destroy(gameObject);                                                                         //destroy the bullet
        }
    }
}
