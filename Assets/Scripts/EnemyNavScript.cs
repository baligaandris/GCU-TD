using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour {

    //private GameObject Goal;
    public GameObject currentWayPoint;
    private Vector3 targetToMoveTo;
    public float speed;

    public float distanceToUni=0;

    // When the enemy is spawned, their goal is set in the navmesh agent, and they immediately start waking towards it.
    void Start () {
        //Goal = GameObject.FindGameObjectWithTag("Goal");
        //GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Goal.transform.position);
        //GetComponent<UnityEngine.AI.NavMeshAgent>().updateRotation = false; //this line makes sure the enemies stay upright and don't rotate around corners. it looks stupid anyway.

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTo, speed*Time.deltaTime);
        if (transform.position == targetToMoveTo) {
            ChangeTargetWaypoint(currentWayPoint.GetComponent<WaypointScript>().nextWayPoint);
        }
        CalculateDistanceToUni();
    }

    public void ChangeTargetWaypoint(GameObject newWaypoint)
    {
        currentWayPoint = newWaypoint;
        targetToMoveTo = new Vector3(currentWayPoint.transform.position.x + Random.Range(-1, 1), 0, currentWayPoint.transform.position.z + Random.Range(-1, 1));
        CalculateDistanceToUni();
    }

    public void CalculateDistanceToUni() {
        distanceToUni = 0;
        distanceToUni += Vector3.Distance(transform.position, currentWayPoint.transform.position);
        GameObject waypointToAddToCalculation;
        waypointToAddToCalculation = currentWayPoint.GetComponent<WaypointScript>().nextWayPoint;

        if (waypointToAddToCalculation != null)
        {
            while (waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint != null)
            {
                distanceToUni += Vector3.Distance(waypointToAddToCalculation.transform.position, waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint.transform.position);
                waypointToAddToCalculation = waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint;

            }
        }
        
    }
}
