using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	private float mx;
    Vector3 neutralPosition;
    // Use this for initialization

    //RaycastHit hit;
    //public float heightAbove;
    //float heightAboveRoad = 0.0f;
    //float diff = 0.0f;

    void Start () {
		//hit = new RaycastHit ();
		mx = 1;
        Vector3 neutralPosition = transform.position; // new Vector3(0, 0.5f, 0);
        print(transform.position + " " + neutralPosition);
	}


    // Update is called once per frame
    void Update () {
        float moveX = Input.GetAxis("MovementX");
        float moveY = Input.GetAxis("MovementY");
        //float rotateX = Input.GetAxis("RotateX");
        //float rotateY = Input.GetAxis("RotateY");
        //Rotate(rotateX, rotateY);
        //print("JP");
        if (transform.position.x == neutralPosition.x && transform.position.z == neutralPosition.z) //Equals(object other)
        {
            //print("yep");
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("MovementX") > 0)
            {

                //if (transform.position == neutralPosition)
                //{
                    transform.position = new Vector3(transform.position.x + mx, transform.position.y, transform.position.z);
               // }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("MovementX") < 0)
            { // (Mathf.Abs(Input.GetAxis("MovementX")) > Mathf.Abs(Input.GetAxis("MovementY")) && Input.GetAxis("MovementX") < 0)
   
                //if (transform.position == neutralPosition)
               // {
                    transform.position = new Vector3(transform.position.x - mx, transform.position.y, transform.position.z);
              //  }
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("MovementY") < 0)
            {
            
               // if (transform.position == neutralPosition)
                //{
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mx);
                //}
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("MovementY") > 0)
            {
             
               // if (transform.position == neutralPosition)
                //{

                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - mx);
                //}
            }
            else
            {
                transform.position = neutralPosition;
            }

        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position = neutralPosition;
        }
        else
        {
            //transform.position = neutralPosition;
        }
        
	}

    public void Rotate(float rotateX, float rotateY)
    {
        if (Input.GetKey(KeyCode.D) || rotateX > 0)
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = Mathf.Lerp(transform.rotation.x, 90, 1f); //Time.deltaTime * 10f
            //transform.Rotate(new Vector3(90, 0, 0f));
            //transform.eulerAngles = rotationVector;
            transform.Rotate(Mathf.Lerp(transform.rotation.x, 90, 100f),0,0);
        }
    }

    public void Move(float moveX, float moveY)
    {

    }

}
