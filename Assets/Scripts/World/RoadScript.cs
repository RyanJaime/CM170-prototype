using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour {

	//public InstantiateCubes instantCubeScript;
	//List<GameObject> cubeList;
	//InstantiateCubes.

	public float factor;

	Mesh mesh;
	Vector3[] vertices;

	Renderer thisRenderer;
	//Matrix4x4 localToWorld;

	// Use this for initialization
	void Start () {
		//cubeList = instantCubeScript._sampleCube;
		mesh = this.GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		//localToWorld = transform.localToWorldMatrix;

		thisRenderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkBlocks ();
		//switchColor ();
	}

	//

	//note: the pairs of vertices must match the sample number, ie 1024 must equal 1024 pairs of vertices (an edge)
	void checkBlocks() {
		for (int i = 0; i < InstantiateCubes._sampleCube.Length; i++) {

			//first vertex of edge
			//Vector3 vecPos1 = localToWorld.MultiplyPoint3x4(vertices [2*i]);
			//Vector3 newVecPos1 = new Vector3 (vecPos1.x, InstantiateCubes._sampleCube [i].transform.localScale.y, vecPos1.z);
			//vertices [2 * i] = newVecPos1;
			float newVal = InstantiateCubes._sampleCube [i].transform.localScale.y/factor;

			if (i == 3) {
				//Debug.Log ("new val " + newVal);
			}
			float oldValue = vertices [2 * i].y;
			float adValue = Mathf.Abs (oldValue) - Mathf.Abs (newVal);
			if (adValue > 0.2f || adValue < -0.2f) {
				newVal /= 2f;
				/*if (i == 3) {
					Debug.Log ("newnew val " + newVal);
				}*/
			}


			vertices [2*i].y = newVal;
			vertices [(2*i)+1].y = newVal;
			//Vector3 vecPos2 = localToWorld.MultiplyPoint3x4(vertices [(2*i)+1]);
			//Vector3 newVecPos2 = new Vector3 (vecPos2.x, InstantiateCubes._sampleCube [i].transform.localScale.y, vecPos2.z);
			//vertices [(2*i)+1] = newVecPos2;
			//vertices [(2*i)+1] = InstantiateCubes._sampleCube [i].transform.position.z;
		}
		mesh.vertices = vertices;

		mesh.RecalculateNormals ();

		Mesh sharedMesh = this.GetComponent<MeshFilter> ().sharedMesh;


		Mesh myMesh = this.GetComponent<MeshFilter>().mesh;
		DestroyImmediate(this.GetComponent<MeshCollider>());
		gameObject.AddComponent<MeshCollider>().sharedMesh = myMesh;
		//collider.sharedMesh = myMesh;

		//change color of mesh based on freq


		//thisRenderer.material.color = 
		//Material origMat = thisRenderer.material;

	}

	/*void switchColor() {
		float newVal = masterFreqGenerator.avgFreq;
		switch (true) {

		case newVal < 400:
			thisRenderer.material.color
			break;

		}
	}*/
}
