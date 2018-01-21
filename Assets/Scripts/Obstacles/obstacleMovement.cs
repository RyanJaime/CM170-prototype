using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //MIDIparse instance = new MIDIparse();
        //List<int[]> MIDIparseOutput = instance.getMIDIList();

        //gameObject

        //collisionScore.isTriggered;
		transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
	}
}
