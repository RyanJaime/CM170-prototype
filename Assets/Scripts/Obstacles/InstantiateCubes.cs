using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour {

	public GameObject _sampleCubePrefab;
	public float distance;
	public static GameObject[] _sampleCube = new GameObject[AudioPeer.sampleNumber];
	public static GameObject[] _sampleCube2 = new GameObject[AudioPeer.sampleNumber];

	public float _maxScale;
	// Use this for initialization
	void Start () {
		

		for (int i = 0; i < AudioPeer.sampleNumber; i++) {
			GameObject _instanceSampleCube = (GameObject)Instantiate (_sampleCubePrefab);
			_instanceSampleCube.transform.position = this.transform.position;
			_instanceSampleCube.transform.parent = this.transform;
			_instanceSampleCube.name = "SampleCube" + i;

			//this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
			_instanceSampleCube.transform.position = new Vector3(-distance, 0, i*0.75f-1);
			_sampleCube [i] = _instanceSampleCube;
		}

		for (int j = 0; j < AudioPeer.sampleNumber; j++) {
			GameObject _instanceSampleCube2 = (GameObject)Instantiate (_sampleCubePrefab);
			_instanceSampleCube2.transform.position = this.transform.position;
			_instanceSampleCube2.transform.parent = this.transform;
			_instanceSampleCube2.name = "SampleCube" + j;

			//this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
			_instanceSampleCube2.transform.position = new Vector3(distance, 0, j*0.75f-1);
			_sampleCube2 [j] = _instanceSampleCube2;
		}
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < AudioPeer.sampleNumber; i++) {
			if (_sampleCube != null) {
				_sampleCube [i].transform.localScale = new Vector3 (10, (AudioPeer._samples[i] * _maxScale) + 2, 10);
			}

			if (_sampleCube != null) {
				_sampleCube2 [i].transform.localScale = new Vector3 (10, (AudioPeer._samples[i] * _maxScale) + 2, 10);
			}
		}
	}
}
