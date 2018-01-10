using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    // Tests the 15notes.bytes file with the MIDIparse script
    // manually filled up a list to test against what MIDIparse returns
    public class MIDIparseTEST {

        [UnityTest]

        public IEnumerator testMIDIparse()
        {
            yield return new WaitForFixedUpdate();

            List<int[]> fifteenNoteList = new List<int[]>();

            int[] CnoteON = { 0, 1, 0, 100, 1 };
            fifteenNoteList.Add(CnoteON);
            int[] CnoteOff = { 100, 0, 0, 0, 1 };
            fifteenNoteList.Add(CnoteOff);
            int[] CsnoteON = { 0, 1, 1, 100, 1 };
            fifteenNoteList.Add(CsnoteON);
            int[] CsnoteOff = { 100, 0, 1, 0, 1 };
            fifteenNoteList.Add(CsnoteOff);
            int[] DnoteON = { 0, 1, 2, 100, 1 };
            fifteenNoteList.Add(DnoteON);
            int[] DnoteOff = { 100, 0, 2, 0, 1 };
            fifteenNoteList.Add(DnoteOff);
            int[] DsnoteON = { 0, 1, 3, 100, 1 };
            fifteenNoteList.Add(DsnoteON);
            int[] DsnoteOff = { 100, 0, 3, 0, 1 };
            fifteenNoteList.Add(DsnoteOff);
            int[] EnoteON = { 0, 1, 4, 100, 1 };
            fifteenNoteList.Add(EnoteON);
            int[] EnoteOff = { 100, 0, 4, 0, 1 };
            fifteenNoteList.Add(EnoteOff);
            int[] FnoteON = { 0, 1, 5, 100, 1 };
            fifteenNoteList.Add(FnoteON);
            int[] FnoteOff = { 100, 0, 5, 0, 1 };
            fifteenNoteList.Add(FnoteOff);
            int[] FsnoteON = { 0, 1, 6, 100, 1 };
            fifteenNoteList.Add(FsnoteON);
            int[] FsnoteOff = { 100, 0, 6, 0, 1 };
            fifteenNoteList.Add(FsnoteOff);
            int[] GnoteON = { 0, 1, 7, 100, 1 };
            fifteenNoteList.Add(GnoteON);
            int[] GnoteOff = { 100, 0, 7, 0, 1 };
            fifteenNoteList.Add(GnoteOff);
            int[] GsnoteON = { 0, 1, 8, 100, 1 };
            fifteenNoteList.Add(GsnoteON);
            int[] GsnoteOff = { 100, 0, 8, 0, 1 };
            fifteenNoteList.Add(GsnoteOff);
            int[] AnoteON = { 0, 1, 9, 100, 1 };
            fifteenNoteList.Add(AnoteON);
            int[] AnoteOff = { 100, 0, 9, 0, 1 };
            fifteenNoteList.Add(AnoteOff);
            int[] AsnoteON = { 0, 1, 10, 100, 1 };
            fifteenNoteList.Add(AsnoteON);
            int[] AsnoteOff = { 100, 0, 10, 0, 1 };
            fifteenNoteList.Add(AsnoteOff);
            int[] BnoteON = { 0, 1, 11, 100, 1 };
            fifteenNoteList.Add(BnoteON);
            int[] BnoteOff = { 100, 0, 11, 0, 1 };
            fifteenNoteList.Add(BnoteOff);
            int[] C2noteON = { 0, 1, 12, 100, 1 };
            fifteenNoteList.Add(C2noteON);
            int[] C2noteOff = { 100, 0, 12, 0, 1 };
            fifteenNoteList.Add(C2noteOff);
            int[] Cs2noteON = { 0, 1, 13, 100, 1 };
            fifteenNoteList.Add(Cs2noteON);
            int[] Cs2noteOff = { 100, 0, 13, 0, 1 };
            fifteenNoteList.Add(Cs2noteOff);
            int[] D2noteON = { 0, 1, 14, 100, 1 };
            fifteenNoteList.Add(D2noteON);
            int[] D2noteOff = { 100, 0, 14, 0, 1 };
            fifteenNoteList.Add(D2noteOff);

            MIDIparse instance = new MIDIparse();
            List<int[]> MIDIparseOutput = instance.getMIDIList();

            Assert.AreEqual(fifteenNoteList, MIDIparseOutput);
        }
    }
}
