using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this is the enemy class, it contains a gameobject, that point to a gameobject in the scene, and a health, that tracks their hp, so we know when they die
public class Enemy
{
    public GameObject EnemyObject;
    public int health;
    public Enemy(GameObject enemyIn, int hp)
    {
        EnemyObject = enemyIn;
        health = hp;
    }
}

public class TowerShootsScript : MonoBehaviour {

    

    public List<Enemy> targets; // this list will contain all the enemies in the range of the tower
    public int damage = 5;
    private float shootCoolDown = 0;
    public float fireRate = 0.3f; //how much time passes between shots.

    public GameObject projectile;


	// Use this for initialization
	void Start () {
        targets = new List<Enemy>(); //run the constuctor of the list
	}
	
	// Update is called once per frame
	void Update () {
        shootCoolDown += Time.deltaTime; //tick the cooldown
        if (targets.Count != 0  && shootCoolDown >=fireRate){ //when the list of targets in range is not empty, and the shooting is not on cooldown we shoot.

            while (targets[0].EnemyObject == null) { // if the target we have on the top of our list is already destroyed, we remove it from the list, and move on to the next target
                    targets.Remove(targets[0]);
                if (targets.Count == 0) { // in case we have just removed the last thing from the list, we exit the loop
                    return;
                }
            }

            targets[0].EnemyObject.GetComponent<EnemyHealthScript>().TakeDamage(damage); //deal the damage
            targets[0].health = targets[0].EnemyObject.GetComponent<EnemyHealthScript>().health; //check in with the enemy to see how much health they actually have

            GameObject newProjectile = Instantiate(projectile,transform.position, Quaternion.identity) as GameObject; //create the projectile
            newProjectile.GetComponent<ProjectileScript>().myTower = gameObject; //tell the projectile where it was shot from

            if (targets[0].health <= 0) { //if the target we were shooting at just died, remove it from the list
                targets.Remove(targets[0]);
            }
            shootCoolDown = 0; //reset cooldown
        }
	}

    void OnTriggerEnter(Collider other) {
        Debug.Log("enemy in range"); 
        if (other.gameObject.tag == "Enemy") //when something enters the range, we check if it is an enemy.
        {
            Enemy newEnemy = new Enemy(other.gameObject, other.gameObject.GetComponent<EnemyHealthScript>().health); //if it is an enemy, we add it to our list, allong with its health
            targets.Add(newEnemy);
        }
    }

    void OnTriggerExit(Collider other) {
        //when an enemy leaves the range, we cycle through our list, find it, and remove it from the list.
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].EnemyObject == other.gameObject) {
                targets.Remove(targets[i]);
            }
        }

    }


}
