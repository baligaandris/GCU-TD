using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDataScript : MonoBehaviour {

    public int uniHealth = 100;

    public Text healthDisplay;

	// Use this for initialization
	void Start () {
        updateUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeDamage(int damage) {
        uniHealth -= damage;
        updateUI();
    }

    void updateUI() {
        healthDisplay.GetComponent<Text>().text = "HP: " + uniHealth;
    }
}
