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
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (transform.position.x < mx) {
				transform.position = new Vector3 (transform.position.x + mx, transform.position.y, transform.position.z);
			}
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (transform.position.x > -mx) {
				transform.position = new Vector3 (transform.position.x - mx, transform.position.y, transform.position.z);
			}
		}
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.z < mx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mx);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.z > -mx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - mx);
            }
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
