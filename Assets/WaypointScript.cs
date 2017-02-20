using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour {

    public GameObject nextWayPoint;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy") {
        //    other.gameObject.GetComponent<EnemyNavScript>().ChangeTargetWaypoint(nextWayPoint);
        //}
    }

}
