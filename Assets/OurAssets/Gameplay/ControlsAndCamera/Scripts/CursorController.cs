using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public Joystick joystick;
    protected Joybuttom joybuttom;
    public float minSpeed = 10f;
    public float upSpeed = 10f;
    public float maxSpeed = 20f;
    public float speedIncrease = 10f;
    private Transform cursor;
    private Transform player;

	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<Joystick>();
        joybuttom = FindObjectOfType<Joybuttom>();
        cursor = GameObject.FindWithTag("Cursor").transform;
        player = GameObject.FindWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {
        var rigidbody = GetComponent<Rigidbody>();
        SpeedController();
//        SpeedControl();
        // rigidbody.velocity = new Vector3(joystick.Horizontal * UpSpeed, rigidbody.velocity.y, joystick.Vertical * UpSpeed);
        Vector3 move = new Vector3(joystick.Horizontal*upSpeed , 0, joystick.Vertical*upSpeed );
        if (Vector3.Distance(player.position, cursor.position) > 5)
            move = new Vector3(0, 0, 0);

        cursor.transform.position = Vector3.Lerp(cursor.position, (cursor.position + move), Time.deltaTime);



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
            upSpeed = maxSpeed;
        }
        else if(!joybuttom.Pressed)
        {
            if(upSpeed>minSpeed)
            {
                upSpeed -= speedIncrease * Time.deltaTime;
            }
            else{
                upSpeed = minSpeed;
            }
        }

    }
}
