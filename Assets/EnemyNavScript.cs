using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour {

    public Transform Goal;

	// Use this for initialization
	void Start () {
        GetComponent<NavMeshAgent>().SetDestination(Goal.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
