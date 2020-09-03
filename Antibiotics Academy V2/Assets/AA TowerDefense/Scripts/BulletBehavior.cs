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

    private float bacteriaPenalty;
    private float virusPenalty;
    private float bacteriaResPenalty;

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
            case "Antibiotic":
                bacteriaPenalty = 0;
                virusPenalty = 100;
                bacteriaResPenalty = 80;
                break;
            case "Water":
                bacteriaPenalty = 80;
                virusPenalty = 0;
                bacteriaResPenalty = 0;
                break;
            case "Fruit":
                bacteriaPenalty = 0;
                virusPenalty = 0;
                bacteriaResPenalty = 0;
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
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                if (target.name == "EnemyBacteria(Clone)")
                {
                    damage = damage - (damage * (bacteriaPenalty / 100));
                    Debug.Log(damage);
                    healthBar.currentHealth -= Mathf.Max(damage, 0);
                }
                
                else if (target.name == "EnemyBacteriaRes(Clone)")
                {
                    damage = damage - (damage * (bacteriaResPenalty / 100));
                    Debug.Log(damage);

                    healthBar.currentHealth -= Mathf.Max(damage, 0);
                }

                else if (target.name == "EnemyVirus(Clone)")
                {
                    damage = damage - (damage * (virusPenalty / 100));
                    Debug.Log(damage);

                    healthBar.currentHealth -= Mathf.Max(damage, 0);
                }

                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    gameManager.Gold += 30;
                }
            }
            Destroy(gameObject);
        }
    }
}
