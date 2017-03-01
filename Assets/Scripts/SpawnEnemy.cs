using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class WavePart {
    public GameObject enemyType;
    public int numberOfEnemies;
    public float howLongTowaitAfter;

    public WavePart(GameObject enemyIn,int howMany, float waitTime) {
        enemyType = enemyIn;
        numberOfEnemies = howMany;
        howLongTowaitAfter = waitTime;    
    }
}
[System.Serializable]
public class Wave {
    public WavePart[] wave;

    public Wave(WavePart[] WavePartsIn) {
        wave = WavePartsIn;
    } 
}


public class SpawnEnemy : MonoBehaviour {
    public Button waveStarterButton;
    public GameObject firstWaypoint;
    public GameObject enemyToSpawn;
    public int numberOfEnemiesToSpawn;

    public Wave[] waveSystem;

    private int currentWave = -1;
    private int currentWavePart = 0;

    private float spawnCoolDown = 0;
    private bool waveInProgress = false;
    private bool speedUp = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        spawnCoolDown -= Time.deltaTime;

        if (waveInProgress)
        {
            if (currentWavePart < waveSystem[currentWave].wave.Length && spawnCoolDown <= 0)
            {
                for (int i = 0; i < waveSystem[currentWave].wave[currentWavePart].numberOfEnemies; i++)
                {
                    GameObject newEnemy = Instantiate(waveSystem[currentWave].wave[currentWavePart].enemyType, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
                    newEnemy.GetComponent<EnemyNavScript>().ChangeTargetWaypoint(firstWaypoint);
                }
                spawnCoolDown = waveSystem[currentWave].wave[currentWavePart].howLongTowaitAfter;
                currentWavePart++;

            }
        }
        if (currentWave >= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentWavePart == waveSystem[currentWave].wave.Length)
            {
                waveStarterButton.GetComponentInChildren<Text>().text = "Let the come!!";
                currentWavePart = 0;
                waveInProgress = false;

                if (currentWave == waveSystem.Length-1) {
                    Application.LoadLevel("EndScene");
                }
            }
        }


    }

    //this is the method called by the button to spawn enemies. it randomizes the position of the spawned enemies, so they don't spawn on top of each other
    public void SpawnEnemies() {
        if (waveInProgress == false && currentWave < waveSystem.Length - 1)
        {
            currentWave++;
            waveInProgress = true;
            waveStarterButton.GetComponentInChildren<Text>().text = ">>";
        }
        else if (speedUp == false) {
            Time.timeScale = 2;
            speedUp = true;
        } else if(speedUp) {
            Time.timeScale = 1;
            speedUp = false;
        }



        //for (int i = 0; i < numberOfEnemiesToSpawn; i++) {
        //    GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
        //    newEnemy.GetComponent<EnemyNavScript>().ChangeTargetWaypoint(firstWaypoint);
        //}
    }
}
