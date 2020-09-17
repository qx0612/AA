using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRange;  //list of enemies that are in range

    private float lastShotTime;              //time of last shot
    private TurretData turretData;       

    // Use this for initialization
    void Start()
    {
        enemiesInRange = new List<GameObject>();       //instantiate the game object list
        lastShotTime = Time.time;                      //set lastShotTime to time.time
        turretData = gameObject.GetComponentInChildren<TurretData>();   //get reference to turret data
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;       //set target to null
        // 1
        float minimalEnemyDistance = float.MaxValue;  //set minimalEnemyDistance to max value
        foreach (GameObject enemy in enemiesInRange)  //foreach  enemy in the enemiesinrange
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();  //set distancetoGoal to the enemy's DistanceToGoal
            if (distanceToGoal < minimalEnemyDistance)     //if distanceToGoal is less than minimalEnemyDistance
            {
                target = enemy;                            //set target to the enemy
                minimalEnemyDistance = distanceToGoal;     //set minimalEnemyDistance to distanceToGoal
            }
        }
        // 2
        if (target != null)             //if target is not null
        {
            if (Time.time - lastShotTime > turretData.CurrentLevel.fireRate)  //if time.time minus lastShotTime is less than the turret's fire rate
            {
                Shoot(target.GetComponent<Collider2D>());  //shoot at the target
                lastShotTime = Time.time;                  //set lastShotTime to time.time
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;  //get the direction between the turret and target
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));               //rotate the turret towards the target
        }
    }

    private void OnEnemyDestroy(GameObject enemy)    //if enemy is destroyed
    {
        enemiesInRange.Remove(enemy);                //remove enemy from the enemiesInRange list
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    private void Shoot(Collider2D target)                                    //function to shoot at target
    {
        GameObject bulletPrefab = turretData.CurrentLevel.bullet;            //bullet prefab is set to the turret's bullet
        // 1 
        Vector3 startPosition = gameObject.transform.position;               //start position is set to the turrets position
        Vector3 targetPosition = target.transform.position;                  //target position is set to the target's position
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = Instantiate(bulletPrefab);                    //instantiate the bullet prefab as newBullet
        newBullet.transform.position = startPosition;                        //newBullet position is set to start position
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>(); 
        bulletComp.target = target.gameObject;                               //set the bullet target to the target game object
        bulletComp.startPosition = startPosition;                            //set the bullet start position to start position
        bulletComp.targetPosition = targetPosition;                          //set the bullet start position to target position
    }

}
