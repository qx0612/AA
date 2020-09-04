using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{

    [HideInInspector]
    public GameObject[] waypoints;             //array of waypoints, which are empty gameobjects
    private int currentWaypoint = 0;           //integer that represents current waypoint
    private float lastWaypointSwitchTime;      
    public float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;    //the current waypoint that they were last at
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;  //the next waypoint they are going to
        // 2 
        float pathLength = Vector2.Distance(startPosition, endPosition);      //pathLength is the distance between the start and end
        float totalTimeForPath = pathLength / speed;                          //totalTimeForPath is pathLength divided by speed
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;         //currentTimeOnPath is current time minus lastWaypointSwitchTime
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);  //updates enemy position by linearly interpolating from start positon to end position by the currentTime divided by totalTime     
        // 3 
        if (gameObject.transform.position.Equals(endPosition))                //if the enemy reaches the end position  
        {
            if (currentWaypoint < waypoints.Length - 2)                       //if the currentWaypoint is not in the last 2 waypoints (because last 2 waypoints is a straight line)
            {
                currentWaypoint++;                                            //currentwaypoint increases by 1
                lastWaypointSwitchTime = Time.time;                           //update lastWayPointSwitchTime to current time

                RotateIntoMoveDirection();                                    //call RotateIntoMoveDireciton
            }
            else
            {
                Destroy(gameObject);                                         //if it reaches the last waypoint, it will destroy the gameobject

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                GameManagerBehavior gameManager =
                    GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
                gameManager.Health -= 1;                                    //player loses a health
            }
        }
    }

    private void RotateIntoMoveDirection()                                  //function to rotate direction
    {
        //1
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;         
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        //3
        GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
        sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    public float DistanceToGoal()                                          //function to calculate the distance to goal
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
