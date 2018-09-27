using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float rollAngle =-71f;//XZ平面的角度控制
    private float rotAngle = 37f;//YZ平面的角度控制

    private float distance = 20;//摄像机与角色的距离

    private Transform player;//跟随角色
    private float roll;//XZ平面的弧度控制

    private float rot;//YZ平面的弧度控制
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
        zoomIn();
    }
    private void LateUpdate()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        roll = rollAngle * Mathf.PI * 2 / 360;//把角度转化为弧度
        rot = rotAngle * Mathf.PI * 2 / 360;


        targetPos = player.position;//目标位置
        Vector3 CameraPos;//定义一个三维向量用来存储摄像机的位置
        float height = distance * Mathf.Sin(rot);//获得摄像机的高度
        float d = distance * Mathf.Cos(rot);
        CameraPos.x = targetPos.x + d * Mathf.Cos(roll);
        CameraPos.y = targetPos.y + height;
        CameraPos.z = targetPos.z + d * Mathf.Sin(roll);

        transform.position = CameraPos;//更新位置
        transform.LookAt(player);//使摄像机对着角色

    }
    void zoomIn()
    {
        ZoomInCamPos = player.position + ZoomPos;
        if (Input.touchCount == 2)
        {
            transform.position = Vector3.Lerp(transform.position, ZoomInCamPos, Time.deltaTime * 3);
        }

    }
}