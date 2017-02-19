using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {
    public int usacValue = 10; //this is out money
    private GameDataScript gameData; // reference to the gamedatascript, the one and only
    public int health = 100; //how much health the enemy has
    public int damage = 1; //how much damage they deal to the Uni, if they reach it

	// Use this for initialization
	void Start () {
        //here we get the gamedatascript, to reference later
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
	}
	
	// Update is called once per frame
	void Update () {
        //if their health reaches 0, they add their value to the money we have, and they get destroyed
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
