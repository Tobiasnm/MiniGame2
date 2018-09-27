using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    public Joystick joystick;
    protected Joybuttom joybuttom;
    public float MinSpeed = 10f;
    public float UpSpeed = 10f;
    public float MaxSpeed = 20f;
    public float SpeedIncrease = 10f;
    private Transform Cursor;
    private CharacterController _Controller;
	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<Joystick>();
        joybuttom = FindObjectOfType<Joybuttom>();
        Cursor = GameObject.FindWithTag("Cursor").transform;

	}
	
	// Update is called once per frame
	void Update () {
        var rigidbody = GetComponent<Rigidbody>();
        SpeedController();
//        SpeedControl();
        // rigidbody.velocity = new Vector3(joystick.Horizontal * UpSpeed, rigidbody.velocity.y, joystick.Vertical * UpSpeed);
        Vector3 move = new Vector3(joystick.Horizontal*UpSpeed , 0, joystick.Vertical*UpSpeed );
        Cursor.transform.position = Vector3.Lerp(Cursor.position, (Cursor.position + move), Time.deltaTime);
	}
    //void SpeedControl()
    //{
    //    if(joybuttom.Pressed )
    //    {
            
    //        if(UpSpeed > MaxSpeed)
    //        {
    //            UpSpeed = MaxSpeed;
    //        }
    //        if(UpSpeed <= MaxSpeed)
    //        {
    //            UpSpeed += SpeedIncrease*Time.deltaTime ; 
    //        }
    //    }
    //    else if(!joybuttom.Pressed)
    //    {
    //        if (UpSpeed < MinSpeed)
    //        {
    //            UpSpeed = MinSpeed;
    //        }
    //        else if(UpSpeed >= MinSpeed)
    //        {
    //            UpSpeed -= SpeedIncrease*Time.deltaTime ;
    //        }
    //    }
            
    //}
    void SpeedController(){
        if(joybuttom.Pressed)
        {
            UpSpeed = MaxSpeed;
        }
        else if(!joybuttom.Pressed)
        {
            if(UpSpeed>MinSpeed)
            {
                UpSpeed -= SpeedIncrease * Time.deltaTime;
            }
            else{
                UpSpeed = MinSpeed;
            }
        }

    }
}
