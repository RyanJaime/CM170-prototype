using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image currentHealthbar;
    public GameManager theGameManager;
    //public Material materialHealthbar;
    public Color highHealthColor;
    public Color halfHealthColor;
    public Color quarterHealthColor;
    //public Text debugText;

    public GameObject audioSource;
    private AudioLowPassFilter lpf;

    public int scorePoints = 1;
    public float dmg = 8;
    public float hitpoints = 100;
    public float maxHitpoint = 100;
    float ratioDmg;

    public int score; // total score
    float cutOffMark;
    float ratioOfMark;

    // Use this for initialization
    void Start()
    {
        score = 0;
        UpdateHealthbar();

        lpf = audioSource.GetComponent<AudioLowPassFilter>();
        lpf.cutoffFrequency = 22000;

        //ie if dmg = 8 and hitpoints = 100, 0.08 percent is taken
        ratioDmg = dmg / maxHitpoint;
    }

    public void UpdateHealthbar()
    {
        float ratio = hitpoints / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        // print("ratio " + ratio);
        //debugText.text = ratio.ToString(); // =(ratio * 100).ToString() + '%';
        if (ratio <= 0.25f) // probably will want to tweak this
        {
            // set Healthbar color to red
            if (currentHealthbar.color != quarterHealthColor)
            {
                currentHealthbar.color = quarterHealthColor;
            }
        }
        else if (ratio <= 0.5f) // probably will want to tweak this
        {
            // set Healthbar color to yellow
            if (currentHealthbar.color != halfHealthColor)
            {
                currentHealthbar.color = halfHealthColor;
            }

        }
        else
        {
            // set Healthbar color to green
            currentHealthbar.color = highHealthColor;
        }
    }

    private void TakeDamage()
    {
        /*
		this.GetComponent<EmitterHit> ().toggleHit ();
		float cutFreqAt = lpf.cutoffFrequency / 1.5f;
		lpf.cutoffFrequency = cutFreqAt;
		//Debug.Log ("cutoff " + lpf.cutoffFrequency);
		hitpoints -= dmg;
		Debug.Log ("hitpoints " + hitpoints);
		if (hitpoints < 0)
		{
			hitpoints = 0;
			//Debug.Log("You are Dead, Dead, Dead");
			theGameManager.RestartGame();
		}
		UpdateHealthbar();
        */
    }

    public void scoreIncrement()
    {
        score += scorePoints;
    }

    private void HealDamage()
    {

    }
}