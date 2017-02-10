﻿using UnityEngine;
using System.Collections;

public class UniScript : MonoBehaviour {

    private GameObject gameData;

	// Use this for initialization
	void Start () {
        gameData = GameObject.FindGameObjectWithTag("GameData");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //when an enemy walks in, deal damage to the uni, and then destroy that enemy.
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            gameData.GetComponent<GameDataScript>().takeDamage(other.gameObject.GetComponent<EnemyHealthScript>().damage);
            Destroy(other.gameObject);
        }
    }
}