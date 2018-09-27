using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour {

    [SerializeField]
    int tapsToBreak = 10;

    int tapsRemaining;
	// Use this for initialization
	void Start () {
        tapsRemaining = tapsToBreak;
	}
	
    public void RegisterTap()
    {
        tapsRemaining -= 1;
    }

    private void DestroyObstacle()
    {
        //Do before destruction


        Destroy(this);
    }

	// Update is called once per frame
	void Update () {


        if (tapsRemaining == 0)
            DestroyObstacle();
	}
}
