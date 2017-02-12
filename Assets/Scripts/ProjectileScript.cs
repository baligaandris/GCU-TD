using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject myTower;
    GameObject target;
    public float speed = 100;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if there are no more targets within range of my tower, just destroy self
        if (myTower.GetComponent<TowerShootsScript>().targets.Count == 0|| myTower.GetComponent<TowerShootsScript>().target == null||myTower.GetComponent<TowerShootsScript>().target.EnemyObject==null)
        {
            Destroy(gameObject);
        }
        else //if there are some, fly towards it. getting the target from the top of the tower's list.
        {

                target = myTower.GetComponent<TowerShootsScript>().target.EnemyObject;
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                transform.rotation = Quaternion.LookRotation(target.transform.position-transform.position);
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (transform.position == target.transform.position)
                {
                    Destroy(gameObject);
                }


            //if you reach your target, destroy self.

        }
    }



}
