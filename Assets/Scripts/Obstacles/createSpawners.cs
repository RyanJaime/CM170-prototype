using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createSpawners : MonoBehaviour {

	public GameObject spawner00;
	public GameObject spawner01;
	public GameObject spawner02;
    public GameObject spawner10;
    public GameObject spawner11;
    public GameObject spawner12;
    public GameObject spawner20;
    public GameObject spawner21;
    public GameObject spawner22;

    public static List<GameObject> spawnerList;
	//public static createSpawners current;

	public static bool ready = false;
	// Use this for initialization
	void Start () {
		spawnerList = new List<GameObject> ();
		GameObject obj00 = Instantiate(spawner00);
		GameObject obj01 = Instantiate(spawner01);
		GameObject obj02 = Instantiate(spawner02);
        GameObject obj10 = Instantiate(spawner10);
        GameObject obj11 = Instantiate(spawner11);
        GameObject obj12 = Instantiate(spawner12);
        GameObject obj20 = Instantiate(spawner20);
        GameObject obj21 = Instantiate(spawner21);
        GameObject obj22 = Instantiate(spawner22);


        spawnerList.Add(obj00);
		spawnerList.Add(obj01);
		spawnerList.Add(obj02);
        spawnerList.Add(obj10);
        spawnerList.Add(obj11);
        spawnerList.Add(obj12);
        spawnerList.Add(obj20);
        spawnerList.Add(obj21);
        spawnerList.Add(obj22);

        if (spawnerList.Count == 9) {
			ready = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
