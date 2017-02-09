using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject enemyToSpawn;
    public int numberOfEnemiesToSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //this is the method called by the button to spawn enemies. it randomizes the position of the spawned enemies, so they don't spawn on top of each other
    public void SpawnEnemies() {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++) {
            Instantiate(enemyToSpawn, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
        }
    }
}
