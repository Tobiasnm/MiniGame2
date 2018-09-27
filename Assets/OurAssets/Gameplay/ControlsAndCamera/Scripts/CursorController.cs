using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    protected Joystick joystick;
    protected Joybuttom joybuttom;
    private float MinSpeed = 5f;
    public float UpSpeed = 5f;
    private float MaxSpeed = 50f;
    private float SpeedIncrease = 5f;
	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<Joystick>();
        joybuttom = FindObjectOfType<Joybuttom>();
	}
	
	// Update is called once per frame
	void Update () {
        var rigidbody = GetComponent<Rigidbody>();
        SpeedControl();
        rigidbody.velocity = new Vector3(joystick.Horizontal * UpSpeed, rigidbody.velocity.y, joystick.Vertical * UpSpeed);
	}

    void SpeedControl()
    {
        if(joybuttom.Pressed)
        {
            
            if(UpSpeed > MaxSpeed)
            {
                UpSpeed = MaxSpeed;
            }
            if(UpSpeed <= MaxSpeed)
            {
                UpSpeed += SpeedIncrease*Time.deltaTime ; 
            }
        }
        else if(!joybuttom.Pressed)
        {
            if (UpSpeed < MinSpeed)
            {
                UpSpeed = MinSpeed;
            }
            else if(UpSpeed >= MinSpeed)
            {
                UpSpeed -= SpeedIncrease*Time.deltaTime ;
            }
        }
            
    }
}
