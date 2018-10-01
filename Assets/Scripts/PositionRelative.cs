using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelative : MonoBehaviour {

	public Transform RelativeObject;
	public float X;
	public float Y;
	public float Z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var pos = RelativeObject.position;
		transform.position = new Vector3(pos.x + X, pos.y + Y, pos.z + Z);	
	}
}
