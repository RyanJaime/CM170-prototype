using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionScore : MonoBehaviour
{
    public void OnTriggerStay(Collider col)
    {
        gameObject.GetComponent<HealthBar>().scoreIncrement();

    }
}
