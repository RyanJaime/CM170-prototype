using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createSpawners : MonoBehaviour {

	public GameObject spawner1;
	public GameObject spawner2;
	public GameObject spawner3;

	public static List<GameObject> spawnerList;
	//public static createSpawners current;

	public static bool ready = false;
	// Use this for initialization
	void Start () {
		spawnerList = new List<GameObject> ();
		GameObject obj1 = Instantiate (spawner1);
		GameObject obj2 = Instantiate (spawner2);
		GameObject obj3 = Instantiate (spawner3);

		spawnerList.Add (obj1);
		spawnerList.Add (obj2);
		spawnerList.Add (obj3);

		if (spawnerList.Count == 3) {
			ready = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
