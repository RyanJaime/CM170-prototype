using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateObstacles : MonoBehaviour {
    // alternative to MIDIparser, for earlier playtesting
    // guarentees equal spacing between drum beats of Square hammer song

    public GameObject obstacle;
    GameObject clone;
    public float distanceBetween = 14.1f; //14.2
    private float startingYPosition = -98.7f;
    private int maxObstacles = 62;

	// Use this for initialization
	void Start () {
        spawnToSnareBeat();
    }
	
    private void spawnToSnareBeat()
    {
        for (int i =0; i < maxObstacles; i++)
        {
            print("distanceBetween: " + distanceBetween + " i: " + i);
            Vector3 spawnPosition = new Vector3(0, (startingYPosition - distanceBetween*i), -1);
            clone = Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
