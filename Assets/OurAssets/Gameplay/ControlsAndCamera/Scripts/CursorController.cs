using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public float maxDistance = 10;

    protected Joystick joystick;
    protected Joybutton joybutton;
    private float MinSpeed = 10f;
    public float UpSpeed = 10f;
    private float MaxSpeed = 20f;
    private float SpeedIncrease = 10f;
    private Transform cursor;
    private Transform player;


    // Use this for initialization
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        cursor = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        SpeedControl();
        // rigidbody.velocity = new Vector3(joystick.Horizontal * UpSpeed, rigidbody.velocity.y, joystick.Vertical * UpSpeed);
        Vector3 move = new Vector3(joystick.Horizontal * UpSpeed, 0, joystick.Vertical * UpSpeed);

        if (Vector3.Distance(cursor.position, player.position) > maxDistance)
            move = new Vector3(0, 0, 0);

        cursor.transform.position = Vector3.Lerp(cursor.position, (cursor.position + move), Time.deltaTime);

    }

    void SpeedControl()
    {
        if (joybutton.Pressed)
        {

            if (UpSpeed > MaxSpeed)
            {
                UpSpeed = MaxSpeed;
            }
            if (UpSpeed <= MaxSpeed)
            {
                UpSpeed += SpeedIncrease * Time.deltaTime;
            }
        }
        else if (!joybutton.Pressed)
        {
            if (UpSpeed < MinSpeed)
            {
                UpSpeed = MinSpeed;
            }
            else if (UpSpeed >= MinSpeed)
            {
                UpSpeed -= SpeedIncrease * Time.deltaTime;
            }
        }

    }
    void ControlMaxDistance()
    {

    }
}
