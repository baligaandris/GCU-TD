using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDataScript : MonoBehaviour {

    public int uniHealth = 100;

    public Text healthDisplay;

    public GameObject activeTower;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        updateUI();
        hit = new RaycastHit();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {

                    if (hit.collider.gameObject.tag == "Tower")
                    {
                        StartCoroutine(SlowlyChangeActiveTower());

                    }
                    else
                    {

                        StartCoroutine(SlowlyCloseRadialMenu());

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

    IEnumerator SlowlyCloseRadialMenu() {
        yield return new WaitForSeconds(0.1f);
        activeTower = null;
    }

    IEnumerator SlowlyChangeActiveTower()
    {
        yield return new WaitForSeconds(0.1f);
        activeTower = hit.collider.gameObject;
    }
}
