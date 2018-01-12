using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateObstacles : MonoBehaviour {
    // alternative to MIDIparser, for earlier playtesting
    // guarentees equal spacing between drum beats of Square hammer song

    public GameObject obstacleY;
    public GameObject obstacleX;
    public GameObject obstacleZ;
    GameObject clone;
    public float distanceBetweenQuarterBeats = 1.0f; //13.7
    private float distanceBetweenTripletBeats = 1f;
    private float distanceBetweenEigthBeats = 1f;

    private float startingYPosition = -98.7f;
    private int maxObstacles = 64;

    private float startingXPosition = 98.7f;
    private float startingZPosition = 93f;

    // Use this for initialization
    void Start () {
        distanceBetweenTripletBeats = (distanceBetweenQuarterBeats * 2) / 3;
        distanceBetweenEigthBeats = distanceBetweenQuarterBeats / 2;
        print(distanceBetweenTripletBeats);
        //print(distanceBetweenEigthBeats);
        spawnToSnareBeat();
    }
	
    //34th note is triplet
    private void spawnToSnareBeat()
    {
        const int beatWithFourEighths = 30;
        const int beatWithFourEighths2 = 38 + 8 + 8 + 8;
        const int beatWithTriplet = 38;
        const int beatWithTriplet2 = 38+8;
        const int beatWithTriplet3 = 38 + 8 + 8;

        int offsetBy1 = 1;

        for (int i =0; i < maxObstacles; i++)
        {
            /*
            switch (i)
            {
                case beatWithFourEighths:
                    break;
                default:
                    break;
            }
            */
            //print("distanceBetween: " + distanceBetweenQuarterBeats + " i: " + i);
            
            
            if (i == beatWithFourEighths || i == beatWithFourEighths2)
            {
                int secondAndFourthNoteXpos = -1;
                if (i == beatWithFourEighths)
                {
                    secondAndFourthNoteXpos = 1;
                }
                clone = Instantiate(obstacleY, transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i), -1), Quaternion.identity);
                clone = Instantiate(obstacleY, transform.TransformPoint(secondAndFourthNoteXpos, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats), -1), Quaternion.identity);
                clone = Instantiate(obstacleY, transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 2), -1), Quaternion.identity);
                clone = Instantiate(obstacleY, transform.TransformPoint(secondAndFourthNoteXpos, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 3), -1), Quaternion.identity);
                //clone.transform.localScale = new Vector3(1f, ticks / 10, 1f);
            }

            else if ( i == beatWithTriplet || i == beatWithTriplet2 || i == beatWithTriplet3)
            {
                //int firstNoteXpos = 0;
                int secondNoteXpos = -1;
                //int thirdNoteXpos = 1;
                if (i == beatWithTriplet2)
                {
                    secondNoteXpos = 1;
                }
                clone = Instantiate(obstacleY, transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i), -1), Quaternion.identity);
                //clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleY, transform.TransformPoint(secondNoteXpos, (startingYPosition - distanceBetweenQuarterBeats * (i+0.5f) ), -1), Quaternion.identity);
                //clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleY, transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * (i+1)), -1), Quaternion.identity);
                //clone.transform.localScale = new Vector3(2, 2, 1f);
            }
           // else if ( i > beatWithTriplet)
            else if (i < beatWithFourEighths || (i > beatWithFourEighths && i < beatWithTriplet) || i > beatWithTriplet+1 || i > beatWithTriplet2+1) //  || i > beatWithFourEighths
            {
                if (offsetBy1 == 1 || offsetBy1 == -1)
                {
                    offsetBy1 = 0;
                }
                else if (offsetBy1 == 0)
                {
                    int quickRand = Random.Range(0, 2);
                    if (quickRand == 0) { quickRand = -1; }
                    offsetBy1 += quickRand;
                }
                Vector3 spawnPosition = new Vector3(offsetBy1, (startingYPosition - distanceBetweenQuarterBeats * i), -1);
                clone = Instantiate(obstacleY, spawnPosition, Quaternion.identity);
            }
            


            /*
            if (i == beatWithFourEighths || i == beatWithFourEighths2)
            {
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * i),0, -1), Quaternion.identity);
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats),0, -1), Quaternion.identity);
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 2),0, -1), Quaternion.identity);
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 3),0, -1), Quaternion.identity);
                //clone.transform.localScale = new Vector3(1f, ticks / 10, 1f);
            }

            else if (i == beatWithTriplet || i == beatWithTriplet2 || i == beatWithTriplet3)
            {
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * i),0, -1), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * (i + 0.5f)),0, -1), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleX, transform.TransformPoint((startingZPosition + distanceBetweenQuarterBeats * (i + 1)),0, -1), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
            }
            // else if ( i > beatWithTriplet)
            else if (i < beatWithFourEighths || (i > beatWithFourEighths && i < beatWithTriplet) || i > beatWithTriplet + 1 || i > beatWithTriplet2 + 1) //  || i > beatWithFourEighths
            {
                Vector3 spawnPosition = new Vector3((startingZPosition + distanceBetweenQuarterBeats * i),0, -1);
                clone = Instantiate(obstacleX, spawnPosition, Quaternion.identity);
            }
            



            if (i == beatWithFourEighths || i == beatWithFourEighths2)
            {
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * i)), Quaternion.identity);
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * i + distanceBetweenEigthBeats)), Quaternion.identity);
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * i + distanceBetweenEigthBeats * 2)), Quaternion.identity);
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * i + distanceBetweenEigthBeats * 3)), Quaternion.identity);
                //clone.transform.localScale = new Vector3(1f, ticks / 10, 1f);
            }

            else if (i == beatWithTriplet || i == beatWithTriplet2 || i == beatWithTriplet3)
            {
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * i)), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * (i + 0.5f))), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
                clone = Instantiate(obstacleZ, transform.TransformPoint(0,0,(startingZPosition + distanceBetweenQuarterBeats * (i + 1))), Quaternion.identity);
                clone.transform.localScale = new Vector3(2, 2, 1f);
            }
            // else if ( i > beatWithTriplet)
            else if (i < beatWithFourEighths || (i > beatWithFourEighths && i < beatWithTriplet) || i > beatWithTriplet + 1 || i > beatWithTriplet2 + 1) //  || i > beatWithFourEighths
            {
                Vector3 spawnPosition = new Vector3(0,0,(startingZPosition + distanceBetweenQuarterBeats * i));
                clone = Instantiate(obstacleZ, spawnPosition, Quaternion.identity);
            }
            */
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
