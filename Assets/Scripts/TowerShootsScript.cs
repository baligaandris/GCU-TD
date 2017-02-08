using UnityEngine;
using System.Collections;

public class TowerShootsScript : MonoBehaviour {

    public GameObject target;
    public int damage = 5;
    private float shootCoolDown = 0;
    public float fireRate = 0.3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        shootCoolDown += Time.deltaTime;
        if (target != null  && shootCoolDown >=fireRate){
            target.GetComponent<EnemyHealthScript>().TakeDamage(damage);
            shootCoolDown = 0;
        }
	}

    void OnTriggerStay(Collider other) {
        Debug.Log("enemy in range");
        if (other.gameObject.tag == "Enemy" && target == null)
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == target) {
            target = null;
        }
    }

}
