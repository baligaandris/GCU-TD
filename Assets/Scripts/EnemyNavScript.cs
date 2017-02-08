using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour {

    private GameObject Goal;

	// Use this for initialization
	void Start () {
        Goal = GameObject.FindGameObjectWithTag("Goal");
        GetComponent<NavMeshAgent>().SetDestination(Goal.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
