using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovement : MonoBehaviour {

    //public 
        float noteSpeed = 0.4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //collisionScore.isTriggered;
		transform.position = new Vector3 (transform.position.x, transform.position.y + noteSpeed, transform.position.z);
	}
}
