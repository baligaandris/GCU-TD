using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuScript : MonoBehaviour {

public GameObject narrativeCanvas;


	IEnumerator DelayLoadMainLevel (){
		GetComponent<AudioSource> ().Play ();
		narrativeCanvas.gameObject.SetActive (true);
		yield return new WaitForSeconds (12);
		Application.LoadLevel ("InterimScene");
	}

	IEnumerator LoadMainLevelAgain (){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.LoadLevel ("InterimScene");
	}

	IEnumerator DelayQuitGame(){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.Quit ();
	}

	IEnumerator DelayLoadStartMenu(){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.LoadLevel ("MainMenuScene");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadMainLevel(){
	
		StartCoroutine (DelayLoadMainLevel ());
	}

	public void QuitGame () {
	
		StartCoroutine (DelayQuitGame ());
	}

	public void LoadStartMenu() {
	
		StartCoroutine (DelayLoadStartMenu ());
	}

	public void RestartLevel() {
		StartCoroutine (LoadMainLevelAgain());
	}

}
