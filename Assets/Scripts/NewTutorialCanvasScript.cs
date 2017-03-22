using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTutorialCanvasScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ContinueButtonClick() {
        if (gameObject.activeInHierarchy)
        {
            //gameObject.transform.parent.
            gameObject.SetActive(false);
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            spawnPoint.GetComponent<SpawnEnemy>().waveInProgress = true;
            //Time.timeScale = 0;
        }
        else
        {
            //gameObject.transform.parent.
            gameObject.SetActive(true);
            //Time.timeScale = 1;
        }
    }
}
