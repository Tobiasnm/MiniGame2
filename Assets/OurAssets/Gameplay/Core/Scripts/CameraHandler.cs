using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

struct CameraState
{
    public float C_Hight; //Camera's hight
    public float C_Distance; //Camera's distance from target.
    public float C_Angle; // Camera's Angle.
    public float C_SmoothSpeed;
    //public float LookatTime;
}

public class CameraHandler : MonoBehaviour
{

    public Transform T_Player; //Player's position
    public Transform target; // Enemy target position
    public float C_Hight = 10f; //Camera's hight
    public float C_Distance = 15f; //Camera's distance from target.
    public float C_Angle = 45; // Camera's Angle.
    public float C_SmoothSpeed = 0.1f;
    //public float LookatTime = 1f;
    //public float stopLookingInSeconds = 10;

    private float E_CameraHight = 5f;
    private float E_CameraDistance = 5f;
    private float E_CameraAngle = 55;
    private Vector3 refVelocity;
    private Vector3 savedCameraPosition;
    //private CameraState savedCameraState;
    //TODO capture state fo the camera in struct 
    void Start()
    {
        NormalView();

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            NormalView();
            CameraLookatAnotherTarget(10);
        }
        else
        {
            NormalView();
            CaptureCameraState();
        }
    }
    // Normally Camera view.
    void NormalView()
    {
        if (!T_Player)
        {
            return;
        }

        Vector3 WorldPosition = (Vector3.forward * -C_Distance) + (Vector3.up * C_Hight);
        //Debug.DrawRay(Target.position, WorldPosition, Color.red);
        Vector3 rotateVector = Quaternion.AngleAxis(C_Angle, Vector3.up) * WorldPosition;
        Vector3 PlayerPosition = T_Player.position;
        //Camera's Y axis is always stable;
        PlayerPosition.y = 0f;
        Vector3 finalPosition = PlayerPosition + rotateVector;

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, C_SmoothSpeed);
        //if no target, look at dog
        if (!target)
            transform.LookAt(PlayerPosition);
    }
    private void OnDrawGizmos()
    {
        if (T_Player)
        {
            Gizmos.DrawLine(transform.position, T_Player.position); //draw a line from player to camera.
            Gizmos.DrawSphere(T_Player.position, 0.5f); //draw a solid sphere with player position and 1.5f radius.
        }
        Gizmos.DrawSphere(transform.position, 0.5f); // draw a solid sphere with camera position.
    }

    public void CameraLookatAnotherTarget(float stopLookingInSeconds = 5)
    {

        if (!T_Player)
        {
            return;
        }

        //CaptureCameraState();
        if (target)
        {
            //transform.LookAt(target);
            
            //var v3T = T_Player.position - target.position;
            //transform.LookAt(target);
            //this.transform.position = Vector3.SmoothDamp(transform.position, T_Player.position + v3T.normalized * E_CameraDistance, ref refVelocity, C_SmoothSpeed);
            Debug.Log("Target; "+target.name);
            if (target.GetComponentInChildren<Outline>())
            {
                target.GetComponentInChildren<Outline>().color = 0;
                Debug.Log("Color changed");
            }
            Invoke("LoadCameraState", stopLookingInSeconds);
        }
        else
        {
            Debug.Log("Warning; no target!");
        }
    }

    private void CaptureCameraState()
    {
        savedCameraPosition = this.transform.position;
        //CameraState state = new CameraState();

        //state.C_Hight = this.C_Hight; //Camera's hight
        //state.C_Distance = this.C_Distance; //Camera's distance from target.
        //state.C_Angle = this.C_Angle; // Camera's Angle.
        //state.C_SmoothSpeed = this.C_SmoothSpeed;
        //state.LookatTime = this.LookatTime;
        //return state;
    }

    private void RemoveTarget()
    {
        if (target && target.GetComponentInChildren<Outline>() != null)
        {
            target.GetComponentInChildren<Outline>().color = 1;
            Debug.Log("Color changed");
        }
        target = null;
    }

    private void LoadCameraState()
    {
        //this.transform.position = Vector3.SmoothDamp(transform.position, savedCameraPosition, ref refVelocity, C_SmoothSpeed);
        RemoveTarget();
        //this.C_Hight = savedCameraState.C_Hight; //Camera's hight
        //this.C_Distance = savedCameraState.C_Distance; //Camera's distance from target.
        //this.C_Angle = savedCameraState.C_Angle; // Camera's Angle.
        //this.C_SmoothSpeed = savedCameraState.C_SmoothSpeed;
        //this.LookatTime = savedCameraState.LookatTime;

    }

}

