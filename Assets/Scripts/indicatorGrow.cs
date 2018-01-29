using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorGrow : MonoBehaviour {

	public GameObject indicator;

	private List<GameObject> indicatorArr = new List<GameObject> ();
	private GameObject clone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
		if (indicatorArr.Count != 0) {
			for (int i = 0; i < indicatorArr.Count; i++) {
				indicatorArr [i].transform.localScale += new Vector3 (0.0018f, 0, 0.0018f);
			}

			if (indicatorArr[0].transform.localScale.x >= 0.09) {
				//transform.localScale = new Vector3(0, 1, 0);
				GameObject toBeDestroyed = indicatorArr [0];
				indicatorArr.Remove (toBeDestroyed);
				Destroy (toBeDestroyed);
			}
        }
    }

	void OnTriggerEnter(Collider col){
		createNewIndicator ();
	}

	void createNewIndicator(){
		Vector3 spawnPosition = new Vector3(0, 0, 0);
		clone = Instantiate(indicator, spawnPosition = new Vector3(indicator.transform.position.x, 0, indicator.transform.position.z), Quaternion.Euler(new Vector3(0,0,0)));
		indicatorArr.Add(clone);
	}
}
