using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerScript : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
    }
}
