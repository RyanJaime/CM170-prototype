using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateObstacles : MonoBehaviour {
    // alternative to MIDIparser, for earlier playtesting
    // guarentees equal spacing between drum beats of Square hammer song

    public GameObject obstacleYred;
    public GameObject obstacleYgrn;
    public GameObject obstacleYblu;

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

    private int offsetBy1;
    private Vector3 adjXZTile;
    private int XZTiles;

    // Use this for initialization
    void Start () {
        distanceBetweenTripletBeats = (distanceBetweenQuarterBeats * 2) / 3;
        distanceBetweenEigthBeats = distanceBetweenQuarterBeats / 2;
        print(distanceBetweenTripletBeats);
        offsetBy1 = 0;
        adjXZTile = new Vector3(0, 0, 0); // first note from below goes to middle tile
        XZTiles = 4;
        //print(distanceBetweenEigthBeats);
        spawnToSnareBeat();
        int yow = 1;
    }

    private GameObject setColorObstacle()
    {
        GameObject coloredObstacle = obstacleYred;
        switch (XZTiles)
        {
            case 0:
            case 1:
            case 2:
                coloredObstacle = obstacleYred;
                break;
            case 3:
            case 4:
            case 5:
                coloredObstacle = obstacleYgrn;
                break;
            case 6:
            case 7:
            case 8:
                coloredObstacle = obstacleYblu;
                break;

        }
                return coloredObstacle;
    }


    private int getIntPlusOrMinusOne() // constrained between -1 and 1
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
        //return offsetBy1;
        return 0;
    }

    private Vector3 randomAdjacentTile()
    {
        int randDirection2 = Random.Range(0, 2);
        int randDirection3 = Random.Range(0, 3);
        switch (XZTiles)
        {
            case 0:
                if (randDirection2 == 0)
                {
                    adjXZTile = new Vector3(0, 0, 1);
                    XZTiles = 1;
                }
                else
                {
                    adjXZTile = new Vector3(-1, 0, 0);
                    XZTiles = 3;
                }
                break;
            case 1:
                if (randDirection3 == 0)
                {
                    adjXZTile = new Vector3(-1, 0, 1);
                    XZTiles = 0;
                }
                else if (randDirection3 == 1)
                {
                    adjXZTile = new Vector3(1, 0, 1);
                    XZTiles = 2;
                }
                else
                {
                    adjXZTile = new Vector3(0, 0, 0);
                    XZTiles = 4;
                }
                break;
            case 2:
                if(randDirection2 == 0)
                {
                    adjXZTile = new Vector3(0, 0, 1);
                    XZTiles = 1;
                }
                else
                {
                    adjXZTile = new Vector3(1, 0, 0);
                    XZTiles = 5;
                }
                break;
            case 3:
                if (randDirection3 == 0)
                {
                    adjXZTile = new Vector3(-1, 0, 1);
                    XZTiles = 0;
                }
                else if (randDirection3 == 1)
                {
                    adjXZTile = new Vector3(0, 0, 0);
                    XZTiles = 4;
                }
                else
                {
                    adjXZTile = new Vector3(-1, 0, -1);
                    XZTiles = 6;
                }
                break;
            case 4:
                int randDirection4 = Random.Range(0, 4);
                if (randDirection4 == 0)
                {
                    adjXZTile = new Vector3(0, 0, 1);
                    XZTiles = 1;
                }
                else if (randDirection4 == 1)
                {
                    adjXZTile = new Vector3(1, 0, 0);
                    XZTiles = 5;
                }
                else if (randDirection4 == 2)
                {
                    adjXZTile = new Vector3(0, 0, -1);
                    XZTiles = 7;
                }
                else if (randDirection4 == 3)
                {
                    adjXZTile = new Vector3(-1, 0, 0);
                    XZTiles = 3;
                }
                break;
            case 5:
                if (randDirection3 == 0)
                {
                    adjXZTile = new Vector3(1, 0, 1);
                    XZTiles = 2;
                }
                else if (randDirection3 == 1)
                {
                    adjXZTile = new Vector3(0, 0, 0);
                    XZTiles = 4;
                }
                else
                {
                    adjXZTile = new Vector3(1, 0, -1);
                    XZTiles = 8;
                }
                break;
            case 6:
                if (randDirection2 == 0)
                {
                    adjXZTile = new Vector3(-1, 0, 0);
                    XZTiles = 3;
                }
                else
                {
                    adjXZTile = new Vector3(0, 0, -1);
                    XZTiles = 7;
                }
                break;
            case 7:
                if (randDirection3 == 0)
                {
                    adjXZTile = new Vector3(-1, 0, -1);
                    XZTiles = 6;
                }
                else if (randDirection3 == 1)
                {
                    adjXZTile = new Vector3(0, 0, 0);
                    XZTiles = 4;
                }
                else
                {
                    adjXZTile = new Vector3(1, 0, -1);
                    XZTiles = 8;
                }
                break;
            case 8:
                if (randDirection2 == 0)
                {
                    adjXZTile = new Vector3(1, 0, 0);
                    XZTiles = 5;
                }
                else
                {
                    adjXZTile = new Vector3(0, 0, -1);
                    XZTiles = 7;
                }
                break;
        }


        return adjXZTile;
    }

    

    private void spawnToSnareBeat()
    {
        const int beatWithFourEighths = 30;
        const int beatWithFourEighths2 = 38 + 8 + 8 + 8;
        const int beatWithTriplet = 38;
        const int beatWithTriplet2 = 38+8;
        const int beatWithTriplet3 = 38 + 8 + 8;


        for (int i =0; i < maxObstacles; i++)
        {
            if (i == beatWithFourEighths || i == beatWithFourEighths2)
            {
                Vector3 spawnPosition1 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition1, Quaternion.identity);
                Vector3 spawnPosition2 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition2, Quaternion.identity);
                Vector3 spawnPosition3 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 2), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition3, Quaternion.identity);
                Vector3 spawnPostion4 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i - distanceBetweenEigthBeats * 3), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPostion4, Quaternion.identity);
                //clone.transform.localScale = new Vector3(1f, ticks / 10, 1f);
            }

            else if ( i == beatWithTriplet || i == beatWithTriplet2 || i == beatWithTriplet3)
            {
                Vector3 spawnPosition1 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * i), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition1, Quaternion.identity);
                Vector3 spawnPosition2 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * (i + 0.5f)), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition2, Quaternion.identity);
                Vector3 spawnPosition3 = transform.TransformPoint(0, (startingYPosition - distanceBetweenQuarterBeats * (i + 1)), 0) + randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition3, Quaternion.identity);
            }

            else if (i < beatWithFourEighths || (i > beatWithFourEighths +1 && i < beatWithTriplet) || i > beatWithTriplet+1 || i > beatWithTriplet2+1 || i > beatWithTriplet3+1 || i > beatWithFourEighths2 +1) //  || i > beatWithFourEighths
            {
                
                Vector3 spawnPosition = new Vector3(0, (startingYPosition - distanceBetweenQuarterBeats * i), 0)+randomAdjacentTile();
                clone = Instantiate(setColorObstacle(), spawnPosition, Quaternion.identity);
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
