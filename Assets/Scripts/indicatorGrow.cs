﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorGrow : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10)) // 5
        {
            //print("Found an object - distance: " + hit.distance);
            transform.localScale += new Vector3(0.0028f *2, 0, 0.0028f *2); // *4
        }
        else
        { 
            transform.localScale = new Vector3(0, 1, 0);
        }
    }
}
