using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject thePlayer;

	// Use this for initialization
	void Start () {
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        //thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("gameOverScene");
        //thePlayer.gameObject.SetActive(true);
    }
}
