using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRadialMenuScript : MonoBehaviour {

    private GameObject activeTower;
    private GameDataScript gameData;

	// Use this for initialization
	void Start () {
        gameData = GameObject.FindWithTag("GameData").GetComponent<GameDataScript>();

    }
	
	// Update is called once per frame
	void Update () {
        activeTower = gameData.activeTower;
        if (activeTower != null)
        {
            GetComponent<RawImage>().enabled = true;
            int numberOfChildren = transform.childCount;
            for (int i = 0; i < numberOfChildren; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            transform.position = Camera.main.WorldToScreenPoint(activeTower.transform.position);
        }
        else {
            int numberOfChildren = transform.childCount;
            GetComponent<RawImage>().enabled = false;
            for (int i = 0; i < numberOfChildren; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

	}
}
