using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	private float mx;
    Vector3 neutralPosition;

    private bool inNeutral;

    // Rotate 
    public float speed = 15;
    private int timesHitHorizontal = 0;
    private int timesHitVertical = 0;
    private int timesHitRolling = 0;
    private int frontFace = 0;


    void Start () {
		//hit = new RaycastHit ();
		mx = 1;
        inNeutral = true;
        neutralPosition = new Vector3(0, 0.5f, 0);  // transform.position;
        //print(transform.position + " " + neutralPosition);
	}

    // Update is called once per frame
    void Update () {
        float moveX = Input.GetAxis("MovementX");
        float moveY = Input.GetAxis("MovementY");

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || (moveX == 0 && moveY == 0))
        {
            timesHitRolling = 0;
            timesHitVertical = 0;
            transform.position = neutralPosition;
        }

        if (Input.GetKey(KeyCode.RightArrow) || moveX > 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            timesHitRolling = -1;
            timesHitVertical = 0;
            changeFace(true);
            transform.position = new Vector3(mx, transform.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || moveX < 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            timesHitRolling = 1;
            timesHitVertical = 0;
            changeFace(false);
            transform.position = new Vector3(-mx, transform.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || moveY < 0 && Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            timesHitRolling = 0;
            timesHitVertical = 1;
            transform.position = new Vector3(0, transform.position.y, mx);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || moveY > 0 && Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            timesHitRolling = 0;
            timesHitVertical = -1;
            transform.position = new Vector3(0, transform.position.y, -mx);
        }
       
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(timesHitVertical * 90, timesHitHorizontal * 90, timesHitRolling * 90), Time.deltaTime * speed);
        
    }

    void changeFace(bool increase)
    {
        if (increase) { frontFace++; }
        else { frontFace--; }

        if (frontFace > 3) { frontFace -= 4; }
        if (frontFace < 0) { frontFace += 4; }
        //print(frontFace);
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
