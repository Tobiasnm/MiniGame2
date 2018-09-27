using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour {

    private CameraHandler cameraHandler;
    public GameObject focusTarget;
    private PlayerHandler handlePlayer;
    public GameObject dogPosition;
    public GameObject fleeButton;
    public GameObject attackButton;
    public GameObject dog;
    public Transform fleePos;
    public Transform attackPos;
    public GameObject cursor;



    void Start () {
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        handlePlayer = dog.GetComponent<PlayerHandler>();
        EndScene();
    }
	
	void Update () {
		
	}

    public void EndScene()
    {
        cameraHandler.T_Player = focusTarget.transform;
        //handlePlayer.StopLocomotion();

        //TODO Play howling sound

    }

    public void Attack()
    {
        cursor.transform.position = attackPos.transform.position;
        fleeButton.SetActive(false);
        attackButton.SetActive(false);
    }

    public void Flee()
    {
        cursor.transform.position = fleePos.transform.position;
        fleeButton.SetActive(false);
        attackButton.SetActive(false);
    }
}
