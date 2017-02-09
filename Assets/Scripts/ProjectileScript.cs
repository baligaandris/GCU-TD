using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject myTower;
    GameObject target;
    public float speed = 100;

    // Use this for initialization
    void Start () {
        //we don't need the coroutine any more, the projectile gets destroyed anyway.
        //StartCoroutine(WaitThenDestroySelf());
	}
	
	// Update is called once per frame
	void Update () {
        //if there are no more targets within range of my tower, just destroy self
        if (myTower.GetComponent<TowerShootsScript>().targets.Count == 0)
        {
            Destroy(gameObject);
        }
        else //if there are some, fly towards it. getting the target from the top of the tower's list.
        {
            target = myTower.GetComponent<TowerShootsScript>().targets[0].EnemyObject;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            //if you reach your target, destroy self.
            if (transform.position == target.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }
    //this is no longer needed, but eh, it can stay :D
    IEnumerator WaitThenDestroySelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
