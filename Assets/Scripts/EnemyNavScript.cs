using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour {

    private GameObject Goal;

	// When the enemy is spawned, their goal is set in the navmesh agent, and they immediately start waking towards it.
	void Start () {
        Goal = GameObject.FindGameObjectWithTag("Goal");
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Goal.transform.position);
        GetComponent<UnityEngine.AI.NavMeshAgent>().updateRotation = false; //this line makes sure the enemies stay upright and don't rotate around corners. it looks stupid anyway.

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
