using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {
    public int usacValue = 10;
    private GameDataScript gameData;
    public int health = 100; //how much health the enemy has
    public int damage = 1; //how much damage they deal to the Uni, if they reach it

	// Use this for initialization
	void Start () {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
	}
	
	// Update is called once per frame
	void Update () {
        //if their health reaches 0, they get destroyed
        if (health <= 0) {
            gameData.ChangeUsac(usacValue);
            Destroy(gameObject);
        }
	}

    //this method is called by the towers to deal damage to the enemy
    public void TakeDamage(int damage) {
        health -= damage;
    }
}
