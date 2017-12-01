using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterFreqGenerator : MonoBehaviour {

	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;

	public int startWait;
	public bool stop;
	public static float avgFreq;
	//public GameObject audioPeer;
	//grab all static params
	//createSpawners.current.spawnerList
	//Audiopeer._samples
	//

	// Use this for initialization
	void Start () {
		//yield return new WaitForSeconds (startWait);
		//StartCoroutine (waveSpawner ());
		stop = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator waveSpawner() {
		//Debug.Log ("Starting routine");
		yield return new WaitForSeconds (startWait);
		//audioPeer.GetComponent<AudioPeer>().playAudio ();
		if (createSpawners.ready) {
			while (!stop) {
				//Debug.Log ("infreq");
				//right now its spawning after x amount of time

				//frequency:
				float freqAmount = 0f;
				float spawnFreq = 0f;
				float freqMax = 0f;
				float freqMin;
				for (int i = 0; i < AudioPeer.sampleNumber; i++) {
					freqAmount += AudioPeer._samples [i];
					if (AudioPeer._samples [i] > freqMax) {
						freqMax = AudioPeer._samples [i];
					}
				}


               
                avgFreq = freqAmount;
				//higher the freq, faster the spawn
				//lower the freq average, longer it takes to spawn
				spawnFreq = 1/((freqAmount / AudioPeer.sampleNumber) * 2000f);

				if (freqMax >= spawnFreq) {
					//spawnWait = ((freqMax / spawnFreq)/100f) + spawnFreq;
					spawnWait = spawnFreq;
				} else {
					spawnWait = spawnFreq + ((freqMax / spawnFreq)/100f);
				}

				if (spawnWait > 1f && avgFreq != 0.0f) {
					spawnWait = 1f;
				}
                
				//spawnWait = avgFreq;
				if (avgFreq > 0.5f) {
					spawnWait = avgFreq;
				} else {
					spawnWait = 1 - avgFreq;
				}
				//Debug.Log("spawnAwait Amount: " + spawnWait);
				//Debug.Log("freqMax: " + freqMax);

				//spawnWait = Random.Range (spawnLeastWait, spawnMostWait);
				yield return new WaitForSeconds (spawnWait);

				//picks random number and chooses the spawner based off of it
				float randomNum = Random.Range (0f, 1f);
				//Debug.Log ("randomNum " + randomNum);
                // 33% chance to spawn from left, middle or right

				if (randomNum <= 0.33f) {
					createSpawners.spawnerList[0].GetComponent<spawner>().createObstacle();
				} else if (0.33f <= randomNum && randomNum < 0.67f) {
					createSpawners.spawnerList[1].GetComponent<spawner>().createObstacle();
				} else if (0.67f <= randomNum) {
					createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle();
				}  
			}

			/*
			 * while (!stop) {
				//right now its spawning after x amount of time

				//fre
				spawnWait = Random.Range (spawnLeastWait, spawnMostWait);
				yield return new WaitForSeconds (spawnWait);

				float randomNum = Random.Range (0f, 1f);
				Debug.Log ("randomNum " + randomNum);
				if (randomNum <= 0.33f) {
					createSpawners.spawnerList[0].GetComponent<spawner>().execute ();
				} else if (0.33f <= randomNum && randomNum < 0.67f) {
					createSpawners.spawnerList[1].GetComponent<spawner>().execute ();
				} else if (0.67f <= randomNum) {
					createSpawners.spawnerList[2].GetComponent<spawner>().execute ();
				}
			}
			*/

		}


	}
}
