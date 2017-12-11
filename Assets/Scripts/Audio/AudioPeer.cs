using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {


    public List<AudioClip> AudioList = new List<AudioClip>();
    public AudioSource _audioSource;

	public static int sampleNumber = 1024;
	public static float[] _samples = new float[sampleNumber];

	public static float[] _freqBand = new float[64];

	public static AudioClip yourAudioClip;

	public GameObject masterGenerator;

	public bool haveSource = false;
	public float duration = 0.0f;

	// Use this for initialization
	void Start () {
        //runAudio();
        _audioSource = GetComponent<AudioSource> ();
        //sampleNumber = 1024;
        //haveSource = false;

        //if (yourAudioClip != null) {
        //setting clip
        //setAudioSource(yourAudioClip);

        setAudioSource(AudioList[0]);
        haveSource = true;

			if (duration > 0.0f) {
                //yield return new WaitForSeconds(3.1f);
                //playAudio();
                StartCoroutine(PlayAudio());

                StartCoroutine (WaitForEnd ());
			}

		
	}

    public void runAudio()
    {
        AudioPeer.yourAudioClip = AudioList[1];
    }


        void DelaySongPlay()
    {
       new WaitForSeconds(3.1f);
    }
	
	// Update is called once per frame
	void Update () {

			
		if (haveSource == true) {
			GetSpectrumAudioSource ();
		}

		//MakeFrequencyBands ();
		
		//if (haveSource && 
	}

	void GetSpectrumAudioSource() {
		//Debug.Log ("getting spec");
		_audioSource.GetSpectrumData (_samples, 0, FFTWindow.Blackman);
	}

	public void setAudioSource(AudioClip audio) {
		_audioSource.clip = audio;
		duration = audio.length;
	}

    IEnumerator PlayAudio()
    {
        if (_audioSource != null)
        {
            yield return new WaitForSeconds(2.3f); //2.73 // 2.91
            _audioSource.Play();
            //StartCoroutine (masterGenerator.GetComponent<masterFreqGenerator> ().waveSpawner());
        }
    }

    
    public void playAudio() {
		if (_audioSource != null) {
			_audioSource.Play ();
			//StartCoroutine (masterGenerator.GetComponent<masterFreqGenerator> ().waveSpawner());
		}
	}

	IEnumerator WaitForEnd() {
		yield return new WaitForSeconds (duration + 2.0f);
		SceneManager.LoadScene("gameWinScene");
	}
	void stopRoutine() {

	}



	void MakeFrequencyBands() {
		/*
		 * 22050/512 = 43 Hz per sample
		 * 
		 * 0-2 = 86hz
		 * 1-4 = 172 Hz, 87 - 258 Hz
		 * 2-8 = 344 Hz, 259 - 602 Hz
		 * 3-16 = 688 Hz, 603 - 1290 Hz
		 * 4-32 = 1376 Hz, 1291-2666 Hz
		 * 5-64 = 2752 Hz, 2667-5418 Hz
		 * 6-128 = 5504 Hz,  5419-10922
		 * 7-256 = 11008 Hz, 10923-21930
		 */
		int count = 0;
		for (int i = 0; i < 64; i++) {

			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;

			if (i == 63) {
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++) {
				average += _samples [count] * (count + 1);
				count++;
			}

			average /= count;

			_freqBand [i] = average * 10;
		}
	}
}
