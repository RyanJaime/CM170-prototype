using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class MIDIparse : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float timeTotal = 0.0f;
    private float ticksTotal = 0;
    private int index = 0;
    private int contbitInt = 0;
    private int lcontbitInt = 0;
    private int mcontbitInt = 0;
    private int rcontbitInt = 0;
    //public List<GameObject> rSpawnerList = new List<GameObject>();
    //public List<GameObject> mSpawnerList = new List<GameObject>();
    //public List<GameObject> lSpawnerList = new List<GameObject>();
    int ti= 0;
    int i = 0;
    int j = 0;
    int k = 0;
    bool rNoteOn = false;
    bool mNoteOn = false;
    bool lNoteOn = false;
    public List<byte> rByteList = new List<byte>();
    //public List<bool> rOnOffList = new List<bool>();
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
        startTime = Time.time;
       
        TextAsset bytesFile = Resources.Load("ah") as TextAsset;
        byte[] data_array = bytesFile.bytes; // Put it into a byte array


        print("Attempting to Parse MIDI!");
        //print("Length:" + data_array.Length); // Total number of bytes 

        if (data_array[0] != 0x4d || data_array[1] != 0x54 || data_array[2] != 0x68 || data_array[3] != 0x64)
        {
            Debug.Log(String.Format("no 'MThd', so it's not a MIDI file\n" +
                                    "{0:x}{1:x} {2:x}{3:x}", data_array[0], data_array[1], data_array[2], data_array[3]));
        }

        getBytes(data_array);

        //foreach(byte item in rByteList) { print("right list" + item);}
        //print("What the bytes look like in Unity" + hexString); // Unity shortens 0x00 to 0x0

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
                        //print("It's bigger than 0x80, value:" + data_array[i - 2]);
                        rByteList.Add(data_array[i - 2]); // adding 8x, if it's there
                    }
                    rByteList.Add(data_array[i - 1]); // then add the rest to 8x or just this by itself
                }
                if (data_array[i + 1] == 0x3d)
                {
                    if (data_array[i - 2] >= 0x80)
                    {
                        //print("It's bigger than 0x80, value:" + data_array[i - 2]);
                        mByteList.Add(data_array[i - 2]); // adding 8x, if it's there
                    }
                    mByteList.Add(data_array[i - 1]); // then add the rest to 8x or just this by itself
                }
                if (data_array[i + 1] == 0x3e)
                {
                    if (data_array[i - 2] >= 0x80)
                    {
                        //print("It's bigger than 0x80, value:" + data_array[i - 2]);
                        lByteList.Add(data_array[i - 2]); // adding 8x, if it's there
                    }
                    lByteList.Add(data_array[i - 1]); // then add the rest to 8x or just this by itself
                }
            }

            // This will print bytes out indivually, in DEC and HEX
            //Debug.Log(String.Format("DEC: {0} and in HEX: {0:X}", data_array[i]));
        }
        return rByteList;
    }
  
    void FixedUpdate() // update every 0.02 ms
    {
        float t = Time.time - startTime;
        float secondsPerFixedUpdate = Time.deltaTime;
        //print("FixedUpdate time : " + Time.deltaTime);
        float TPQ = 960; // default for REAPER MIDI
        float BPM = 120; // default for REAPER MIDI
        float ms = 60000 / (BPM * TPQ);
        float s = ms / 1000;
        //print("MS:        " + ms);   // 0.5208333 ms for 1 tick. 1000 ms = 1 s
        //print("S:        " + s);   // 0.0005208333 s for 1 tick. 1000 ms = 1 s
        // about 1920 ticks per second
        // 26.041665 FixedUpdates for 1 tick
        string secondsString = t.ToString("F6"); // (t % 60)
        string msString = (ms % 60).ToString();
        timerText.text = secondsString;

        float ticksPerFixedUpdate = secondsPerFixedUpdate / s;

        //print("ticksPerFixedUpdate: "+ ticksPerFixedUpdate); // 38.4 with default REAPER TPQ and BPM
        

        // stuff on delta times
        if (index < lByteList.Count && lByteList[index] > 0x80)
        {
            lcontbitInt = calculateContinuationBit(lByteList);
        } else if (index < lByteList.Count) { oneByteDeltaTime(lByteList, 0, lNoteOn); }

        if (index < mByteList.Count && mByteList[index] > 0x80)
        {
            mcontbitInt = calculateContinuationBit(mByteList);
        } else if (index < mByteList.Count) { oneByteDeltaTime(mByteList, 1, mNoteOn); }

        if (index < rByteList.Count && rByteList[index] > 0x80)
        {
            rcontbitInt = calculateContinuationBit(rByteList);
        } else if (index < rByteList.Count) { oneByteDeltaTime(rByteList, 2, rNoteOn); }



        // compare contbitBA to ticks 
        if (lcontbitInt > 0 && ticksTotal >= lcontbitInt)
        {
            spawnBasedOnContBitsDeltaTime(lcontbitInt, 0, lNoteOn);
        }

        if (mcontbitInt > 0 && ticksTotal >= mcontbitInt)
        {
            spawnBasedOnContBitsDeltaTime(mcontbitInt, 1, mNoteOn);
        }

        if (rcontbitInt > 0 && ticksTotal >= rcontbitInt) 
        {
        spawnBasedOnContBitsDeltaTime(rcontbitInt,2, rNoteOn);

            /*
            // spawning cuz a continuation bit delta time
            if (!rNoteOn || !mNoteOn || !lNoteOn)
            {
                print("spawning cuz a continuation bit delta time");
                if (!lNoteOn) {
                    createSpawners.spawnerList[0].GetComponent<spawner>().createObstacle();
                    lcontbitInt = 0;
                }
                if (!mNoteOn)
                {
                    createSpawners.spawnerList[1].GetComponent<spawner>().createObstacle();
                    mcontbitInt = 0;
                }
                if (!rNoteOn)
                {
                    createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle();
                    rcontbitInt = 0;
                    rNoteOn = true;
                }
                //rNoteOn = true;
                ticksTotal = 0;
                contbitInt = 0; 
                index += 2;
            }
            else
            {
                print("note was on, cont bytes");
                rNoteOn = false;
                ticksTotal = 0;
                contbitInt = 0;
                rcontbitInt = 0;
                index += 2;
            }
            */
        }

        else { ticksTotal += ticksPerFixedUpdate; }


        //if (rNoteOn) { createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle(); }
        if (lNoteOn) { createSpawners.spawnerList[0].GetComponent<spawner>().createObstacle(); }
        if (mNoteOn) { createSpawners.spawnerList[1].GetComponent<spawner>().createObstacle(); }
        if (rNoteOn) { createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle(); }


        //print("ticksTotal " + ticksTotal);
    }

    public void spawnBasedOnContBitsDeltaTime(int theContbitInt, int spawnNum, bool noteOn)
    {
        // compare contbitBA to ticks 
        if (theContbitInt > 0 && ticksTotal >= theContbitInt) //
        {
            // spawning cuz a continuation bit delta time 
            if (!noteOn)
            {
                print("spawning cuz a continuation bit delta time");
                if (!noteOn)
                {
                    createSpawners.spawnerList[spawnNum].GetComponent<spawner>().createObstacle();
                    theContbitInt = 0;
                }
                
                //rNoteOn = true;
                ticksTotal = 0;
                theContbitInt = 0;
                index += 2;
            }
            else
            {
                print("note was on, cont bytes");
                //rNoteOn = false;
                ticksTotal = 0;
                theContbitInt = 0;
                //rcontbitInt = 0;
                index += 2;
            }

            if (spawnNum == 0) { lNoteOn = noteOn; lcontbitInt = theContbitInt; }
            else if (spawnNum == 1) { mNoteOn = noteOn; mcontbitInt = theContbitInt; }
            else if (spawnNum == 2) { rNoteOn = noteOn; rcontbitInt = theContbitInt; }
        }
    }

    public void oneByteDeltaTime(List<byte> deltaTimes,int spawnNum, bool noteOn)
    {
        // 1 byte delta times
        int ticksToWait = deltaTimes[index];

        if (ticksTotal > ticksToWait) //(ticksToWait >= ticksTotal)
        {
            //if (spawnNum == 2)
            if (!noteOn)
            {
                // spawning cuz a 1 byte delta time
                print("spawning cuz a 1 byte delta time. ticksToWait " + ticksToWait);
                if (!noteOn) { createSpawners.spawnerList[spawnNum].GetComponent<spawner>().createObstacle(); }
                noteOn = true;
                ticksTotal = 0;
                index++;
            }
            else
            {
                print("note was on, 1 byte");
                noteOn = false;
                ticksTotal = 0;
                index++;
            }

            if (spawnNum == 0) { lNoteOn = noteOn; }
            else if (spawnNum == 1) { mNoteOn = noteOn; }
            else if (spawnNum == 2) { rNoteOn = noteOn; }

        }
    }

    public int calculateContinuationBit(List<byte> deltaTimes) {
        // Continuation bit stuff on delta times
        
        //print("byte > x80");
        //Debug.Log(String.Format("hex and dec " + "{0:x}  {0:d}", rByteList[index]));
        var result = Convert.ToString(deltaTimes[index], 2); // convert to binary 
        //print(result + "RESULT");
        byte trunkated8 = (byte)(deltaTimes[index] - 0x80);
        byte[] LHSbyteArray = new byte[1] { trunkated8 };
        BitArray LHSbits = new BitArray(LHSbyteArray);
        //print("LHSbits before: " + LHSbits[0] + " " + LHSbits[7]);
        Reverse(LHSbits);
        //print("LHSbits after: " + LHSbits[0] + " " + LHSbits[7]);

        byte trunkatedMSB = (byte)deltaTimes[index + 1]; // remove msb somehow
        byte[] RHSbyteArray = new byte[1] { trunkatedMSB };

        BitArray RHSbits = new BitArray(RHSbyteArray);
        Reverse(RHSbits);

        BitArray contbitBA = new BitArray(16);
        contbitBA[0] = false; contbitBA[1] = false; // left
        //for (int i = 2; i < contbitBA.Length; i++)
        for (int i = contbitBA.Length - 1; i > -1; i--) // starts at 15
        {
            if (i > 1 && i < 9 && LHSbits[i - 1]) // 7 bits from LHSbits
            {
                contbitBA[i] = LHSbits[i - 1];
            }

            // skip 9th bit completely

            else if (i > 8 && RHSbits[i - 8]) // 7 bits from RHSbits
            {
                contbitBA[i] = RHSbits[i - 8];
            }
        }

        Reverse(contbitBA);
        contbitInt = getIntFromBitArray(contbitBA); // number of ticks to wait!
        //print("contbitInt: " + contbitInt);
        return contbitInt;
    }

    public void Reverse(BitArray array)
    {
        int length = array.Length;
        int mid = (length / 2);

        for (int i = 0; i < mid; i++)
        {
            bool bit = array[i];
            array[i] = array[length - i - 1];
            array[length - i - 1] = bit;
        }
    }

    private int getIntFromBitArray(BitArray bitArray)
    {

        if (bitArray.Length > 32)
            throw new ArgumentException("Argument length shall be at most 32 bits.");

        int[] array = new int[1];
        bitArray.CopyTo(array, 0);
        return array[0];

    }


    void Update()
    {
        

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