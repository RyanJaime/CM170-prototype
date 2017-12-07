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
    float ticksTotal = -100;
    private int index = 0;
    private int checkerIndex = 0;

    private int giantIndex = 0;
    bool noteOnKeepSpawning = false;
    bool noteOn00 = false; bool firstSpawnofNoteOn00 = false;
    bool noteOn01 = false; bool firstSpawnofNoteOn01 = false;
    bool noteOn02 = false; bool firstSpawnofNoteOn02 = false;
    bool noteOn10 = false; bool firstSpawnofNoteOn10 = false;
    bool noteOn11 = false; bool firstSpawnofNoteOn11 = false;
    bool noteOn12 = false; bool firstSpawnofNoteOn12 = false;
    bool noteOn20 = false; bool firstSpawnofNoteOn20 = false;
    bool noteOn21 = false; bool firstSpawnofNoteOn21 = false;
    bool noteOn22 = false; bool firstSpawnofNoteOn22 = false;
    int inc = 0;

    private int contbitInt = 0;
    private int lcontbitInt = 0;
    private int mcontbitInt = 0;
    private int rcontbitInt = 0;
    float ticksPerFixedUpdate = 0;
    bool updateIncrementTicks = true;

    int lindex =0;
    int mindex=0;
    int rindex=0;

    int ti= 0;
    int i = 0;
    int j = 0;
    int k = 0;
    bool rNoteOn = false;
    bool mNoteOn = false;
    bool lNoteOn = false;

    // Each int array will hold
    // (deltaTime in dec, On / Off, what note)
    public List<int[]> oneGiantByteList = new List<int[]>();

    public List<int> anotherBigOneForDebugging = new List<int>();

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
        startTime = Time.time;
       
        TextAsset bytesFile = Resources.Load("cool") as TextAsset;
        byte[] data_array = bytesFile.bytes; // Put it into a byte array

        print("Attempting to Parse MIDI!");
        //print("Length:" + data_array.Length); // Total number of bytes 

        if (data_array[0] != 0x4d || data_array[1] != 0x54 || data_array[2] != 0x68 || data_array[3] != 0x64)
        {
            Debug.Log(String.Format("no 'MThd', so it's not a MIDI file\n" +
                                    "{0:x}{1:x} {2:x}{3:x}", data_array[0], data_array[1], data_array[2], data_array[3]));
        }

        getBytes(data_array);

        float t = Time.time - startTime;
        float secondsPerFixedUpdate = Time.deltaTime;
        //print("FixedUpdate time : " + Time.deltaTime);
        float TPQ = 100; // default for REAPER MIDI is 960
        float BPM = 120; // default for REAPER MIDI is 120
        float ms = (60000 / (BPM * TPQ));
        float s = ms / 1000;

        //print("MS:        " + ms);   // 0.5208333 ms for 1 tick. 1000 ms = 1 s
        //print("S:        " + s);   // 0.0005208333 s for 1 tick. 1000 ms = 1 s
        // about 1920 ticks per second
        // 26.041665 FixedUpdates for 1 tick
        string secondsString = t.ToString("F6"); // (t % 60)
        string msString = (ms % 60).ToString();
        ticksPerFixedUpdate = secondsPerFixedUpdate / s;

        //foreach(byte item in rByteList) { print("right list" + item);}
        //print("What the bytes look like in Unity" + hexString); // Unity shortens 0x00 to 0x0

    }

    //public List<byte> getBytes(byte[] data_array)
    // adds to the left, middle, right byte lists
    public void getBytes(byte[] data_array)
    {
        for (int i = 22; i < data_array.Length; i++)
        {
            hexString += data_array[i].ToString("x"); // concat to string in hex format
            if (i % 2 == 1) { hexString += ' '; } // add a space after every 2 byte buddies

            if (data_array[i] == 0x90 || data_array[i] == 0x80)
            {
                //print("should add to oneGiantList");
                int isNoteOn = 0;
                List<byte> tempContByteList = new List<byte>();
                int deltaTime = 0;
                if (i-2 > 21 && data_array[i - 2] >= 0x80)
                {
                    tempContByteList.Add(data_array[i - 2]);
                }

                tempContByteList.Add(data_array[i - 1]);

                if (tempContByteList.Count > 1)
                {
                    deltaTime = calculateContinuationBit(tempContByteList);
                }
                else
                {
                    deltaTime = tempContByteList[0];
                }

                if (data_array[i] == 0x90) { isNoteOn = 1; }
                else if (data_array[i] == 0x80) { isNoteOn = 0; }

                int lane = data_array[i + 1] - 60;
               
                int[] newOneEveryTime = new int[3];
                newOneEveryTime[0] = deltaTime;
                newOneEveryTime[1] = isNoteOn;
                newOneEveryTime[2] = lane;


                print("array "+newOneEveryTime[0] + " " + newOneEveryTime[1] + " " + newOneEveryTime[2]);

                oneGiantByteList.Add(newOneEveryTime);

                anotherBigOneForDebugging.Add(newOneEveryTime[0]);
                anotherBigOneForDebugging.Add(newOneEveryTime[1]);
                anotherBigOneForDebugging.Add(newOneEveryTime[2]);
            }


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
        }
    }

    void FixedUpdate() // update every 0.02 ms
    {
        //timerText.text = secondsString;

        //print("ticksPerFixedUpdate: " + ticksPerFixedUpdate); // 38.4 with default REAPER TPQ and BPM

        // function checks next event\s delta time
        // when current ticks has reached that delta time, it turns note on.
        // bool is set to note on. bool tells function not to check again until next event's delta time has been reached.

        // check if ticksTotal >= any spawner's next delta time

        // delta time, isnoteon, lane

        // check if next note is spawning at the same time, if it is, call function again

        if (ticksTotal < 0)
        {
            ticksTotal += ticksPerFixedUpdate;
            print("ticksTotal after " + ticksTotal);
            return;
        }

            int[] currentIntArray;
        int[] nextIntArray;
        int numberOfNotesTogether = 1;


        if (checkerIndex < oneGiantByteList.Count - 4)
        {
            currentIntArray = oneGiantByteList[checkerIndex];
            nextIntArray = oneGiantByteList[checkerIndex+1];

            if (nextIntArray[0] == 0)
            {
               numberOfNotesTogether = 2;
            }
        }

        for (i = 0; i < numberOfNotesTogether; i++)
        {
            spawnornot();
        }


        if (noteOn00) { createSpawners.spawnerList[0].GetComponent<spawner>().createObstacle(); }
        if (noteOn01) { createSpawners.spawnerList[1].GetComponent<spawner>().createObstacle(); }
        if (noteOn02) { createSpawners.spawnerList[2].GetComponent<spawner>().createObstacle(); }
        if (noteOn10) { createSpawners.spawnerList[3].GetComponent<spawner>().createObstacle(); }
        if (noteOn11) { createSpawners.spawnerList[4].GetComponent<spawner>().createObstacle(); }
        if (noteOn12) { createSpawners.spawnerList[5].GetComponent<spawner>().createObstacle(); }
        if (noteOn20) { createSpawners.spawnerList[6].GetComponent<spawner>().createObstacle(); }
        if (noteOn21) { createSpawners.spawnerList[7].GetComponent<spawner>().createObstacle(); }
        if (noteOn22) { createSpawners.spawnerList[8].GetComponent<spawner>().createObstacle(); }

        print("ticksTotal after " + ticksTotal);
    }

    public void spawnornot()
    {
        if (giantIndex < oneGiantByteList.Count)
        {
            int[] localIntArray = oneGiantByteList[giantIndex];

            if (ticksTotal >= localIntArray[0])
            {
                if (localIntArray[1] == 1)
                {
                    print("Note ON in lane: " + localIntArray[2] + " at ticks: " + localIntArray[0]);
                    createSpawners.spawnerList[localIntArray[2]].GetComponent<spawner>().createObstacle();
                    noteOnKeepSpawning = true;
                }
                else if (localIntArray[1] == 0)
                {
                    print("Note OFF in lane: " + localIntArray[2] + " at ticks: " + localIntArray[0]);
                    //createSpawners.spawnerList[localIntArray[2]].GetComponent<spawner>().createObstacle();
                    noteOnKeepSpawning = false;
                }

                giantIndex++;
                ticksTotal = 0;
                //spawnornot();
                // do note on or off and reset ticks
            }
            else { ticksTotal += ticksPerFixedUpdate; }

            if (localIntArray[2] == 0) { noteOn00 = noteOnKeepSpawning; firstSpawnofNoteOn00 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 1) { noteOn01 = noteOnKeepSpawning; firstSpawnofNoteOn01 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 2) { noteOn02 = noteOnKeepSpawning; firstSpawnofNoteOn02 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 3) { noteOn10 = noteOnKeepSpawning; firstSpawnofNoteOn10 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 4) { noteOn11 = noteOnKeepSpawning; firstSpawnofNoteOn11= noteOnKeepSpawning; }
            else if (localIntArray[2] == 5) { noteOn12 = noteOnKeepSpawning; firstSpawnofNoteOn12 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 6) { noteOn20 = noteOnKeepSpawning; firstSpawnofNoteOn20 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 7) { noteOn21 = noteOnKeepSpawning; firstSpawnofNoteOn21 = noteOnKeepSpawning; }
            else if (localIntArray[2] == 8) { noteOn22 = noteOnKeepSpawning; firstSpawnofNoteOn22 = noteOnKeepSpawning; }
        }
    }

    public int calculateContinuationBit(List<byte> deltaTimes) {
        // Continuation bit stuff on delta times
        //print("byte > x80");
        //Debug.Log(String.Format("hex and dec " + "{0:x}  {0:d}", rByteList[index]));
        var result = Convert.ToString(deltaTimes[index], 2); // convert to binary 
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
        // for (int i = 2; i < contbitBA.Length; i++)
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
        
    }

    /*
    public bool oneByteDeltaTime(int indy, List<byte> deltaTimes, int spawnNum, bool noteOn)
    {
        // 1 byte delta times
        int ticksToWait = deltaTimes[indy];
        bool incrementTicks = true;

        if (ticksTotal > ticksToWait) // (ticksToWait >= ticksTotal)
        {
            if (!noteOn)
            {
                // spawning cuz a 1 byte delta time
                print("turning note on, lane: " + spawnNum);
                createSpawners.spawnerList[spawnNum].GetComponent<spawner>().createObstacle();
                noteOn = true;
                //ticksTotal = 0;
                //localTicks = 0;
                //incrementTicks = false;
                indy++;
            }
            else
            {
                print("turning note off, lane: " + spawnNum);
                noteOn = false;
                //ticksTotal = 0;
                //localTicks = 0;
                incrementTicks = false;
                indy++;
            }

            if (spawnNum == 0) { lNoteOn = noteOn; lindex = indy; }
            else if (spawnNum == 1) { mNoteOn = noteOn; mindex = indy; }
            else if (spawnNum == 2) { rNoteOn = noteOn; rindex = indy; }
        }
        return incrementTicks;
    }
    
    
    public float spawnBasedOnContBitsDeltaTime(float localTicks, int theContbitInt, int spawnNum, bool noteOn)
    {
        // compare contbitBA to ticks 
        if (theContbitInt > 0 && localTicks >= theContbitInt) //
        {
            // spawning cuz a continuation bit delta time 
            if (!noteOn)
            {
                print("spawning cuz a continuation bit delta time");
                
                createSpawners.spawnerList[spawnNum].GetComponent<spawner>().createObstacle();

                localTicks = 0;
                theContbitInt = 0;
                noteOn = true;
                index += 2;
            }
            else
            {
                //print("note has not ended, continue spawning (from spawnBasedOnContBitsDeltaTime)");
                //print("idk what's happening");

                localTicks = 0;
                theContbitInt = 0;
                noteOn = false;
                index += 2;
            }

            if (spawnNum == 0) { lNoteOn = noteOn; lcontbitInt = theContbitInt; }
            else if (spawnNum == 1) { mNoteOn = noteOn; mcontbitInt = theContbitInt; }
            else if (spawnNum == 2) { rNoteOn = noteOn; rcontbitInt = theContbitInt; } //ticksTotal = localTicks; }
        }
        return localTicks;
    }
    */
}