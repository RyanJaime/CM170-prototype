using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionScore : MonoBehaviour
{
    private float scaleDown;
    private int timesTriggered;
    bool isTriggered;

    private void Start()
    {
        isTriggered = false;
        timesTriggered = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    public void OnTriggerStay(Collider col)
    {
        timesTriggered++;
        gameObject.GetComponent<HealthBar>().scoreIncrement();
        //print("y scale: " + col.transform.localScale.y + " timesTriggered: " + timesTriggered);
        scaleDown = col.transform.localScale.y - 0.5f;
        if (scaleDown == 0)
        {
            col.transform.localScale = new Vector3(transform.localScale.x, scaleDown, transform.localScale.z);
        }
        else
        {
            col.transform.localScale = new Vector3(transform.localScale.x, scaleDown, transform.localScale.z);
            //col.transform.localScale = new Vector3(1, scaleDown, 1);
        }
    }
    public void OnTriggerExit(Collider col)
    {
        isTriggered = false;
    }
}
