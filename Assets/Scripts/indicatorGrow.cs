using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorGrow : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20))
        {
            print("Found an object - distance: " + hit.distance);
            transform.localScale += new Vector3(0.0028f, 0, 0.0028f);
        }
        else
        {
            transform.localScale = new Vector3(0, 1, 0);
        }
    }
}
