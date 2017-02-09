using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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

    

    public List<Enemy> targets;
    public int damage = 5;
    private float shootCoolDown = 0;
    public float fireRate = 0.3f;

    public GameObject projectile;


	// Use this for initialization
	void Start () {
        targets = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
        shootCoolDown += Time.deltaTime;
        if (targets.Count != 0  && shootCoolDown >=fireRate){

            while (targets[0].EnemyObject == null) {
                    targets.Remove(targets[0]);
                if (targets.Count == 0) {
                    return;
                }
            }

            targets[0].EnemyObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
            targets[0].health = targets[0].EnemyObject.GetComponent<EnemyHealthScript>().health;

            GameObject newProjectile = Instantiate(projectile,transform.position, Quaternion.identity) as GameObject;
            newProjectile.GetComponent<ProjectileScript>().myTower = gameObject;

            if (targets[0].health <= 0) {
                targets.Remove(targets[0]);
            }
            shootCoolDown = 0;
        }
	}

    void OnTriggerEnter(Collider other) {
        Debug.Log("enemy in range");
        if (other.gameObject.tag == "Enemy")
        {
            Enemy newEnemy = new Enemy(other.gameObject, other.gameObject.GetComponent<EnemyHealthScript>().health);
            targets.Add(newEnemy);
        }
    }

    void OnTriggerExit(Collider other) {

        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].EnemyObject == other.gameObject) {
                targets.Remove(targets[i]);
            }
        }

    }


}
