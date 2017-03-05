using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffingTower : MonoBehaviour {
    public GameObject [] Towers = new GameObject [16];
    

	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("Buffing Tower" + buffedDamage);
        
        if (collider.gameObject.tag == "Tower")
        {
            TowerShootsScript dmg = collider.gameObject.GetComponentInChildren<TowerShootsScript>();
            dmg.damage += collider.gameObject.GetComponentInChildren<TowerShootsScript>().damage * 0.2f;
            TowerShootsScript spd = collider.gameObject.GetComponentInChildren<TowerShootsScript>();
            spd.fireRate -= collider.gameObject.GetComponentInChildren<TowerShootsScript>().fireRate * 0.2f;

            
            Debug.Log(dmg.damage);
            Debug.Log(spd.fireRate);
            


           // Buff();

        }
    }
}

  

