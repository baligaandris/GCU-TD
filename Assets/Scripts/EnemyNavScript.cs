using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour {

    //private GameObject Goal;
    public GameObject currentWayPoint;
    private Vector3 targetToMoveTo;
    public float speed;
    public float runawaySpeed;

    public float distanceToUni=0;

    // When the enemy is spawned, the spawner tells them their first waypoint
    void Start () {
        

    }

    // Update is called once per frame
    void Update()
    {
        // the enemy moves towards the target at a constant speed
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTo, speed*Time.deltaTime);
        //if it reaches the target location
        if (transform.position == targetToMoveTo) {
            //ask the current waypoint for the next waypoint
            if (GetComponent<EnemyHealthScript>().runningAway) {
                Destroy(gameObject);
            }
            else
            {
                ChangeTargetWaypoint(currentWayPoint.GetComponent<WaypointScript>().nextWayPoint);
            }

            
        }
        //Calculate remaining distance to university
        if (GetComponent<EnemyHealthScript>().runningAway == false)
        {
            CalculateDistanceToUni();
        }
    }

    //this is cript is called when we want to change the target to move towards
    public void ChangeTargetWaypoint(GameObject newWaypoint)
    {
        //first, we do the change
        currentWayPoint = newWaypoint;
        //second, we determine the randomized target, so not all enemies line up, and walk to the exact same target. it adds a slight variation
        targetToMoveTo = new Vector3(currentWayPoint.transform.position.x + Random.Range(-1, 1), 0, currentWayPoint.transform.position.z + Random.Range(-1, 1));
        //lastly, we recalculate our distance to the uni
        CalculateDistanceToUni();
    }

    //this is how we calculat the distance
    public void CalculateDistanceToUni() {
        //first set the distance to 0. we will add to it step by step
        distanceToUni = 0;
        //The first thing to add, is our distance from the current waypoint
        distanceToUni += Vector3.Distance(transform.position, currentWayPoint.transform.position);
        //then we prep for adding all the other waypoints. For this we need a variable to use to cycle through all the waypoints. We set its value to our current waypoint.
        //we want to add its distance from the waypoint after it.
        GameObject waypointToAddToCalculation;
        waypointToAddToCalculation = currentWayPoint;

        //So while there is waypoint to move on to, we keep adding their distance to our number. when they are done, we have the accurate distance to the uni. :)
        while (waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint != null)
        {
            distanceToUni += Vector3.Distance(waypointToAddToCalculation.transform.position, waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint.transform.position);
            waypointToAddToCalculation = waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint;
        }

        
    }

    public void Runaway() {
        speed = runawaySpeed;
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        GameObject closestExitPoint = exitPoints[0];
        for (int i = 1; i < exitPoints.Length; i++) {
            if (Vector3.Distance(transform.position,closestExitPoint.transform.position) > Vector3.Distance(transform.position, exitPoints[i].transform.position)){
                closestExitPoint = exitPoints[i];
            }
        }
        targetToMoveTo = closestExitPoint.transform.position;
    }
}
