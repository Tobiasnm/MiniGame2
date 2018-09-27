using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This class represents a frame in the new FrameDB approach.
 * This frames will be stored in a database that will be fetched in runtime for picking the most
    suitable one for the current situation.
    */
public class Frame {

    #region Attributes
    private int index;
    private float direction;
    private float speed;

    private List<int> neigh;
    #endregion

    /* Constructor */
    public Frame(int i, float d, float v, List<int> n) {
        index = i;
        direction = d;
        speed = v;
        neigh = n;
    }

    public Frame(int i, float d, float v)
    {
        index = i;
        direction = d;
        speed = v;
        neigh = new List<int>();
    }

    public int GetIndex() { return index; }

    public float GetDirection() { return direction; }

    public float GetSpeed() { return speed; }

    public List<int> GetNeighbours() { return neigh; }

    public void AddFrameToNeigh(int index) { neigh.Add(index); }

}
