using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

public Transform tutorialMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tutorialMenu.gameObject.activeInHierarchy == true) {
			Time.timeScale = 0;
		}
	}

	public void ContinueGame() {
		tutorialMenu.gameObject.SetActive (false);
		Time.timeScale = 1;
	}
}
