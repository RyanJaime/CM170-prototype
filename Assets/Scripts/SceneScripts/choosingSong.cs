using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choosingSong : MonoBehaviour {


	public List<AudioClip> AudioList = new List<AudioClip>();
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

	public void runAudio(int key) {
		//im using a list to store AudioSongs because im too lazy to do little hacks to turn this into a more efficient function
		//i would use a dictionary, ie public Dictionary<int, AudioClip> AudioList 

		for (int i = 0; i < key; i++) {
			Debug.Log ("iterate: " + i);
			AudioPeer.yourAudioClip = AudioList [i];
		}

		SceneManager.LoadScene ("demoScene");
	}
}
