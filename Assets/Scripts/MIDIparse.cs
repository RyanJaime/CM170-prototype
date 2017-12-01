using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class MIDIparse : MonoBehaviour
{
    //public List<GameObject> rSpawnerList = new List<GameObject>();
    //public List<GameObject> mSpawnerList = new List<GameObject>();
    //public List<GameObject> lSpawnerList = new List<GameObject>();
    int ti= 0;
    int i = 0;
    int j = 0;
    int k = 0;
    bool rNoteOn = false;
    bool mNoteOn = true;
    bool lNoteOn = true;
    public List<byte> rByteList = new List<byte>();
    public List<byte> mByteList = new List<byte>();
    public List<byte> lByteList = new List<byte>();
    // Debugging. Fill with bytes, compare how they look in Unity, Sublime
    string hexString = "";
    // Load the .bytes (MIDI) file as TextAsset
    // might want to check if it's actually a .bytes file before attempting to load?
    
    

    GameObject clone;
    
    // Use this for initialization
    void Start()
    {
        TextAsset bytesFile = Resources.Load("doot") as TextAsset;
        byte[] data_array = bytesFile.bytes; // Put it into a byte array


        print("Attempting to Parse MIDI!");
        print("Length:" + data_array.Length); // Total number of bytes 

        if (data_array[0] != 0x4d || data_array[1] != 0x54 || data_array[2] != 0x68 || data_array[3] != 0x64)
        {
            Debug.Log(String.Format("no 'MThd', so it's not a MIDI file\n" +
                                    "{0:x}{1:x} {2:x}{3:x}", data_array[0], data_array[1], data_array[2], data_array[3]));
        }

        getBytes(data_array);

        foreach(byte item in rByteList)
        {
            print("right list" + item);
        }
        
        print("What the bytes look like in Unity" + hexString); // Unity shortens 0x00 to 0x0

        //spawn();
    }

    public List<byte> getBytes(byte[] data_array)
    {
        for (int i = 0; i < data_array.Length; i++)
        {
            hexString += data_array[i].ToString("x"); // concat to string in hex format
            if (i % 2 == 1) { hexString += ' '; } // add a space after every 2 byte buddies

            if (data_array[i] == 0x90 || data_array[i] == 0x80)
            {
                if (data_array[i + 1] == 0x3c)
                {
                     // adding the ticks, how long til spawn number
                    //print(data_array[i - 2]);
                    if (data_array[i - 2] >= 0x80)
                    {
                        print("It's bigger than 0x80, value:" + data_array[i - 2]);
                        rByteList.Add(data_array[i - 2]); // adding 8x, if it's there
                    }
                    rByteList.Add(data_array[i - 1]); // then add the rest to 8x or just this by itself

                }
                if (data_array[i + 1] == 0x3d)
                {
                    mByteList.Add(data_array[i - 1]);
                }
                if (data_array[i + 1] == 0x3e)
                {
                    lByteList.Add(data_array[i - 1]);
                }
            }

            // This will print bytes out indivually, in DEC and HEX
            //Debug.Log(String.Format("DEC: {0} and in HEX: {0:X}", data_array[i]));
        }
        return rByteList;
    }

    void spawnSpawn()
    {
        
    }
     
    void Update()
    {

        if (i < rByteList.Count && rByteList[i] <= ti)
        {
            //print("Spawn Right      time: " + ti + " wait for right:" + rByteList[i]);
            ti = 0;
            if (rNoteOn) {
                // if noteOn, start spawning for t tpq
                createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle();
                i++;
                rNoteOn = false;
            }
            rNoteOn = true;
        } else { ti++; }

        /*
        else if (j < mByteList.Count)
        {
            if (mByteList[j] <= ti)
            {
                print("SPAWN MIDDLE      time: " + ti + " wait for mid:" + mByteList[j]);
                ti = 0;
                createSpawners.spawnerList[1].GetComponent<spawner>().createObstacle();
                ti += 3;
                j++;
            }

            else if (k < lByteList.Count)
            {
                if (lByteList[k] <= ti)
                {
                    print("SPAWN LEFT       time: " + ti + " wait for left:" + lByteList[k]);
                    ti = 0;
                    createSpawners.spawnerList[0].GetComponent<spawner>().createObstacle();
                    ti += 3;
                    j++;
                }
            }
        }
        }
        
        ti += 3;
        */
    }

}