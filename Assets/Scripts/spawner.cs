﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

	public GameObject obstacle;
	public Vector3 spawnValues;

	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;

	public int startWait;
	public int spawnPos;

	public bool stop;

	List<GameObject> obstacleArr = new List<GameObject>();
	GameObject clone;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitSpawner ());
	}
	
	// Update is called once per frame
	void Update () {
		spawnWait = Random.Range (spawnLeastWait, spawnMostWait);

		if (obstacleArr.Count > 0) {
			if (obstacleArr [0].transform.position.z < -10) {
				GameObject toBeDestroyed = obstacleArr [0];
				obstacleArr.Remove (toBeDestroyed);
				Destroy (toBeDestroyed);
			}
		}
	}

	IEnumerator waitSpawner(){
		yield return new WaitForSeconds (startWait);

		while(!stop){
			Vector3 spawnPosition = new Vector3 (0,0,0);
			clone = Instantiate (obstacle, spawnPosition = transform.TransformPoint (spawnPos, 0, 0), gameObject.transform.rotation);
			obstacleArr.Add (clone);
			yield return new WaitForSeconds (spawnWait);
		}
	}

	//execute params means that whatever the masterFreqGen obj sends to a particular spawner, execute those parameters
	void executeParams() {

	}
}
