﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	IEnumerator DelayLoadMainLevel (){
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Application.LoadLevel ("PrototypeScene");
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
}