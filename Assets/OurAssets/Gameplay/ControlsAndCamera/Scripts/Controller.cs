using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public float SpeedControl = 10f;
    Touch touch;
    public Vector2 startPos;
    public Vector2 direction;
    public GameObject player;
    RaycastHit hitInfo = new RaycastHit();
    private Camera mainCamera;
    public float angle;
    UnityEngine.AI.NavMeshAgent agent;

    // Bit shift the index of the layer (9) to get a bit mask
    int layerMask = 1 << 9;
    
    private bool canMove = true;

    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }

    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }

        set
        {
            agent = value;
        }
    }
    public Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    void Start()
    {
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        mainCamera = Camera.main;
        angle = mainCamera.GetComponent<CameraHandler>().C_Angle;

    }

    void FixedUpdate()
    {
        //if (!canMove)
        //    return;

//#if UNITY_EDITOR

//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, layerMask))
//                Agent.destination = hitInfo.point;
//        }

//#endif




        // Track a single touch as a direction control.
        if (Input.touches.Length >0)
        {
            Touch touch = Input.GetTouch(0);
            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position -startPos;
                    direction = Rotate(direction, 360 - angle);
                        break;

                case TouchPhase.Ended:
                        direction = new Vector2(0,0);
                    break;
            }
            transform.position = (player.transform.position + new Vector3(direction.x, 0, direction.y).normalized * SpeedControl);
        }
      


    }


}




