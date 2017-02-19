using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRadialMenuScript : MonoBehaviour {

    private GameObject activeTower; //this is where we will temporarily store the active tower. it needs to be updated every frame, to make sure we do stuff to the right tower.
    private GameDataScript gameData; //this is where we will store a reference to our game data script

	// Use this for initialization
	void Start () {
        gameData = GameObject.FindWithTag("GameData").GetComponent<GameDataScript>(); 
        //we get the gamedata script for later reference

    }
	
	// Update is called once per frame
	void Update () {
        //we get the active tower to make sure it we are doing things to the right tower.
        activeTower = gameData.activeTower;
        //if there is an active tower, we make the menu visible. this means the circle and all the little buttons attached to it as children. 
        if (activeTower != null)
        {
            GetComponent<RawImage>().enabled = true;
            int numberOfChildren = transform.childCount;
            for (int i = 0; i < numberOfChildren; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            //we also move it to sit on top of the active tower
            transform.position = Camera.main.WorldToScreenPoint(activeTower.transform.position);
        }
        else {
            //if there is no active tower, we turn this menu, and all children of it off
            int numberOfChildren = transform.childCount;
            GetComponent<RawImage>().enabled = false;
            for (int i = 0; i < numberOfChildren; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

	}

    public void Button1Click() {
        Debug.Log("Button clicked");

    }
}
