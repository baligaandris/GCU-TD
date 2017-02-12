using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this is the enemy class, it contains a gameobject, that point to a gameobject in the scene, and a health, that tracks their hp, so we know when they die
[System.Serializable]
public class Enemy
{
    public GameObject EnemyObject;
    public int health;
    public float distanceToUni;
    
    public Enemy(GameObject enemyIn, int hp, float dist)
    {
        EnemyObject = enemyIn;
        health = hp;
        distanceToUni = dist;
    }
}

public class TowerShootsScript : MonoBehaviour {


    public Enemy target;
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
        CleanUpDestroyedTargets();
        UpdateDistancesFromUni();
        DetermineNewTarget();
        shootCoolDown += Time.deltaTime; //tick the cooldown
        if (targets.Count != 0 && shootCoolDown >= fireRate)
        {



            //selecting the target closest to the uni




            if (target.EnemyObject != null)
            {
                target.EnemyObject.GetComponent<EnemyHealthScript>().TakeDamage(damage); //deal the damage
                target.health = target.EnemyObject.GetComponent<EnemyHealthScript>().health; //check in with the enemy to see how much health they actually have

                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject; //create the projectile
                newProjectile.GetComponent<ProjectileScript>().myTower = gameObject; //tell the projectile where it was shot from

                if (target.health <= 0)
                { //if the target we were shooting at just died, remove it from the list
                    targets.Remove(targets[0]);
                }
                shootCoolDown = 0; //reset cooldown
            }

        }

            
        }
	

    void OnTriggerEnter(Collider other) {
        //Debug.Log("enemy in range"); 
        if (other.gameObject.tag == "Enemy") //when something enters the range, we check if it is an enemy.
        {
            Enemy newEnemy = new Enemy(other.gameObject, other.gameObject.GetComponent<EnemyHealthScript>().health,other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance); //if it is an enemy, we add it to our list, allong with its health, and distance from the Uni
            targets.Add(newEnemy);
        }
    }

    void OnTriggerExit(Collider other) {
        //when an enemy leaves the range, we cycle through our list, find it, and remove it from the list.
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].EnemyObject == other.gameObject) {
                targets.Remove(targets[i]);
                Debug.Log("enemy out of range");

                CleanUpDestroyedTargets();
                UpdateDistancesFromUni();
                DetermineNewTarget();
            }



        }

    }

    void DetermineNewTarget() {
        if (targets.Count == 0)
        {
            target = null;
        }
        else {
            target = targets[0];
        }

        for (int i = 0; i < targets.Count; i++)
        {

                if (target.distanceToUni > targets[i].distanceToUni)
                {
                    //Debug.Log("I changed the target");
                    if (targets[i].EnemyObject != null)
                    {
                        target = targets[i];
                    }
                }

        }


    }

    void CleanUpDestroyedTargets() {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].EnemyObject == null)
            {
                targets.Remove(targets[i]);
                i--;
                //Debug.Log("i fixed a removed target");
            }
        }
    }

    void UpdateDistancesFromUni() {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].distanceToUni = targets[i].EnemyObject.GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance;
        }
    } 




}
