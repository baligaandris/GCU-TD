﻿using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {

    public int usacValue = 10; //this is the money value of this enemy
    private GameDataScript gameData; // reference to the gamedatascript, the one and only
    public float health = 100; //how much health the enemy starts with
    public float currentHealth; // how much health the enemy has
    public float damage = 1; //how much damage they deal to the Uni, if they reach it
    public bool runningAway = false;
    public bool isBoss = false;
    public GameObject EnemyToSpawn;

    public delegate void HealthChanged(float currentHealth, float health);
    public event HealthChanged OnHealthChanged;
    // Use this for initialization
    void Start () {
        //here we get the gamedatascript, to reference later
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update () {
        //if their health reaches 0, they add their value to the money we have, and they get destroyed
        if (health <= 0 && runningAway == false) {
            runningAway = true;
            GetComponent<EnemyNavScript>().Runaway();
            currentHealth = 0;
            health = 0;
           // if(isBoss == true)
           // {
          //      Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
                //GetComponent<EnemyNavScript>().currentWayPoint += EnemyToSpawn.GetComponent<EnemyNavScript>().currentWayPoint;
          //  }
        }
	}

    //this method is called by the towers to deal damage to the enemy
    public void TakeDamage(float damage) {
        health -= damage;
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth, health);
        }
    }
}
