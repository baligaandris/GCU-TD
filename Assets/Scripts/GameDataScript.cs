using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDataScript : MonoBehaviour {

    public int uniHealth = 100;

    public Text healthDisplay;

    public GameObject activeTower;

	// Use this for initialization
	void Start () {
        updateUI();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("used raycast");
                if (hit.collider.gameObject.tag == "Tower")
                {
                    activeTower = hit.collider.gameObject;
                    Debug.Log("Clicked on Tower");
                }
                else {
                    activeTower = null;
                }
            }
        }

        StartCoroutine (DelayLoadEndScene());
	}

    //we call this script, when an enemy walks into the Uni, to deal damage to it.
    public void takeDamage(int damage) {
        uniHealth -= damage;
        updateUI();

    }

    //we will have more things to display, we can keep updating this method, to use it to update all UI elements.
    void updateUI() {
        healthDisplay.GetComponent<Text>().text = "HP: " + uniHealth;
    }

	IEnumerator DelayLoadEndScene (){
		if (uniHealth <= 0) {
			yield return new WaitForSeconds (2);
			Application.LoadLevel ("EndScene");
		}
	}
}
