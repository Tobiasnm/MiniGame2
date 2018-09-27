using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBarController : MonoBehaviour {
    Scrollbar bar;
    private Object Cursor;
    private float UpdateSpeed;
	// Use this for initialization
	void Start () {
        bar = FindObjectOfType<Scrollbar>();
        Cursor = FindObjectOfType<CursorController>();

	}
	
	// Update is called once per frame
	void Update () {
        //if(System.Math.Abs(System.Math.Abs(Cursor.GetComponent<CursorController>().UpSpeed) - System.Math.Abs(bar.GetComponent<CursorController>().MinSpeed)) < 0)
        //{
        //    bar.size = 0f;
        //}
        //else if(System.Math.Abs(bar.GetComponent<CursorController>().UpSpeed) > System.Math.Abs(bar.GetComponent<CursorController>().MinSpeed))
        //{
        //    bar.size += 0.3f;
        //}
        //else if(System.Math.Abs(bar.GetComponent<CursorController>().UpSpeed) == System.Math.Abs(bar.GetComponent<CursorController>().MaxSpeed))
        //{
        //    bar.size = 1f;
        //}
	}
}
