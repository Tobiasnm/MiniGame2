﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorController : MonoBehaviour
{

    public static Joystick joystick;
    public bool isRunning = false;
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float maxDistanceFromPlayer = 5;
    public float speedIncrease = 10f;
    private Transform cursor;
    private Transform player;
    private JoystickButton joybutton;
    private float speedModifier = 10f;
    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<JoystickButton>();
        cursor = GameObject.FindWithTag("Cursor").transform;
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        SpeedController();
        rigidbody.velocity = new Vector3(joystick.Horizontal * speedModifier, 0, joystick.Vertical * speedModifier);
        if (Vector3.Distance(player.position, cursor.position) > maxDistanceFromPlayer)
            rigidbody.velocity = new Vector3(0, 0, 0);
        if(rigidbody.velocity != new Vector3(0,0,0))
        {
            isRunning = true;
        }
        else{
            isRunning = false;
        }

        //Vector3 move = new Vector3(joystick.Horizontal*speedModifier , 0, joystick.Vertical*speedModifier );
        //if (Vector3.Distance(player.position, cursor.position) > maxDistanceFromPlayer)
        //    move = new Vector3(0, 0, 0);

        //cursor.transform.position = Vector3.Lerp(cursor.position, (cursor.position + move), Time.deltaTime);

    }


    void SpeedController()
    {
        if (joybutton.Pressed)
        {
            speedModifier = maxSpeed;
        }
        else if (!joybutton.Pressed)
        {
            if (speedModifier > minSpeed)
            {
                speedModifier -= speedIncrease * Time.deltaTime;
            }
            else
            {
                speedModifier = minSpeed;
            }
        }

    }
}
