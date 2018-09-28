using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float rollAngle =-79.7f;//XZ
    public float rotAngle = 45.4f;//YZ

    public float distance = 12.6f;//the distance from camera and player

    private Transform player;
    private float roll;//XZ

    private float rot;//YZ
    private Vector3 ZoomInCamPos;
    private Vector3 targetPos;
    public Vector3 ZoomPos;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ZoomIn();
    }
    private void LateUpdate()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        roll = rollAngle * Mathf.PI * 2 / 360;
        rot = rotAngle * Mathf.PI * 2 / 360;


        targetPos = new Vector3(player.position.x,player.position.y,player.position.z+3f) ;//Target position
        Vector3 CameraPos;//camera position
        float height = distance * Mathf.Sin(rot);//hight of camera
        float d = distance * Mathf.Cos(rot);
        CameraPos.x = targetPos.x + d * Mathf.Cos(roll);
        CameraPos.y = targetPos.y + height;
        CameraPos.z = targetPos.z + d * Mathf.Sin(roll);

        transform.position = CameraPos;//update position
        transform.LookAt(targetPos);

    }
    void ZoomIn()
    {
        ZoomInCamPos = player.position + ZoomPos;
        if (Input.touchCount == 2)
        {
            transform.position = Vector3.Lerp(transform.position, ZoomInCamPos, Time.deltaTime * 3);
        }

    }
    private void OnDrawGizmos()
    {
        if (player)
        {
            Gizmos.DrawLine(transform.position, targetPos); //draw a line from player to camera.
            Gizmos.DrawSphere(targetPos, 0.5f); //draw a solid sphere with player position and 1.5f radius.
        }
        Gizmos.DrawSphere(transform.position, 0.5f); // draw a solid sphere with camera position.
    }
}