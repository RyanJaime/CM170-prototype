using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterHit : MonoBehaviour {

	public ParticleSystem emitSpark;
	private ParticleSystem.EmissionModule emo;

	void Start() {
		emo = emitSpark.emission;
		emo.enabled = false;
		//emitSpark.GetComponent<ParticleSystem> ().emission.enabled = false;
	}

	public void toggleHit () {
		Debug.Log ("working emitter");
		//emitSpark.GetComponent<ParticleSystem> ().emission.enabled = true;
		emo.enabled = true;
		StartCoroutine (stopSparkle ());

	}

	IEnumerator stopSparkle() {
		yield return new WaitForSeconds (0.1f);
		emo.enabled = false;
		//emitSpark.GetComponent<ParticleSystem> ().emission.enabled = false;
	}
}
