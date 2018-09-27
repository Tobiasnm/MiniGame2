using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/* This is the frame database class. It will store frame info to be used in the new velocity/rotation approach.*/
[CreateAssetMenu(fileName = "FrameDB", menuName = "FrameDataBase", order = 1)]
public class FrameDB : ScriptableObject{

    #region Attributes
    private List<Frame> frames = createDB();

    private static string longTakeFileName = "take1_15NN 1.csv";
    #endregion

    // Constructor
    /*
    public FrameDB() {
        frames = new List<Frame>();
    }
    */

    public void InitializeDB() {
        frames = new List<Frame>();
    }


    public List<Frame> GetAllFrames() { return frames; }

    public Frame GetFrame(int index) { return frames[index]; }

    public void AddFrame(Frame f) {
        frames.Add(f);
    }

    
    public static List<Frame> createDB() {
        return CSV_Reader.ReadFile(longTakeFileName);
    }
}
