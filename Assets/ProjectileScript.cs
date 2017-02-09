using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject myTower;
    GameObject target;
    public float speed = 100;

    // Use this for initialization
    void Start () {
        //StartCoroutine(WaitThenDestroySelf());
	}
	
	// Update is called once per frame
	void Update () {
        if (myTower.GetComponent<TowerShootsScript>().targets.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            target = myTower.GetComponent<TowerShootsScript>().targets[0].EnemyObject;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            if (transform.position == target.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator WaitThenDestroySelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
