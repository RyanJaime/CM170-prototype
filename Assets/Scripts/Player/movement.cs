using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	private float mx;
	// Use this for initialization

	RaycastHit hit;
	public float heightAbove;
	float heightAboveRoad = 0.0f;
	float diff = 0.0f;
	void Start () {
		hit = new RaycastHit ();
		mx = 1;
	}

	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("MovementX");
        float moveY = Input.GetAxis("MovementY");


        // > < < > 0

        if (Input.GetKey (KeyCode.RightArrow) || Input.GetAxis("MovementX") > 0)
        {
            //if (transform.position.x < mx) {
            if (transform.position == new Vector3(0, 0.5f, 0))
            {
                transform.position = new Vector3 (transform.position.x + mx, transform.position.y, transform.position.z);
			}
		}
		else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetAxis("MovementX") < 0)
        { // (Mathf.Abs(Input.GetAxis("MovementX")) > Mathf.Abs(Input.GetAxis("MovementY")) && Input.GetAxis("MovementX") < 0)
            //if (transform.position.x > -mx) {
            if(transform.position == new Vector3(0, 0.5f, 0)) { 
				transform.position = new Vector3 (transform.position.x - mx, transform.position.y, transform.position.z);
			}
		}
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("MovementY") < 0)
        {
            //if (transform.position.z < mx){
            if (transform.position == new Vector3(0, 0.5f, 0))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mx);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("MovementY") > 0)
        {
            //if (transform.position.z > -mx){
            if (transform.position == new Vector3(0, 0.5f, 0))
            {

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - mx);
            }
        }

        
        else
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }
        
        /* ROAD BUMPS
        if (Physics.Raycast (transform.position, Vector3.down, out hit, 200f)) {
			heightAboveRoad = hit.distance;
			//Debug.Log ("Height above: " + heightAboveRoad);
			if (heightAboveRoad != heightAbove) {
				Vector3 newPos = new Vector3 (transform.position.x, transform.position.y - heightAboveRoad + heightAbove, transform.position.z);
				transform.position = newPos;
			}
		}
        */
	}

	void OnTriggerEnter(Collider collisionInfo){
		//print ("here");
	}
}
