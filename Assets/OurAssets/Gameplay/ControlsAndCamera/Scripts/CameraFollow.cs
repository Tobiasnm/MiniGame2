using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //orignal camera postion.
    public float rollAngle =-79.7f;//XZ
    public float rotAngle = 45.4f;//YZ
    public float distance = 12.6f;//the distance from camera and player

    private Transform player;
    private Transform cursor;
    private float roll;//XZ
    private float rot;//YZ
    private Vector3 targetPos;

    private float zoomInRollAngleValue = -88.2f;
    private float zoomInrotAngleValue = 20f;
    private float zoomIndistanceValue = 5f;

    private float zoomOutRollAngleValue = -40f;
    private float zoomOutrotAngleValue = 50f;
    private float zoomOutdistanceValue = 20f;

    public float rotRollR = -180f;
    public float rotRotR = 20f;
    public float rotDisR = 5f;

    public float rotRollL = 30f;
    public float rotRotL = 20f;
    public float rotDisL = 5f;

    private Vector3 rotatCamPosR;
    private Vector3 rotatCamPosL;
    private float speed = 10f;
    public Vector3 zoomInpos;
    public Vector3 zoomOutpos;

    private bool accessisRunning;

    private Vector3 CameraPos;//camera position  


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        cursor = GameObject.FindWithTag("Cursor").transform;
    }
    private void LateUpdate()
    {
        UpdatePosition(); 
        Zooming();
        RotationController();
    }

    public void UpdatePosition()
    {
        roll = rollAngle * Mathf.PI * 2 / 360;
        rot = rotAngle * Mathf.PI * 2 / 360;

        targetPos = new Vector3(player.position.x,player.position.y,player.position.z) ;//Target position


        float height = distance * Mathf.Sin(rot);//hight of camera
        float d = distance * Mathf.Cos(rot);
        CameraPos.x = targetPos.x + d * Mathf.Cos(roll);
        CameraPos.y = targetPos.y + height;
        CameraPos.z = targetPos.z + d * Mathf.Sin(roll);

        transform.position = CameraPos;//update position
       // RotationController();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
        transform.LookAt(targetPos);
    }

     void Zooming()
    {
        //zoom in
        if (cursor.GetComponent<CursorController>().isRunning == true)
        {

            StartCoroutine(ChangeRotAngleValue(rotAngle, zoomInrotAngleValue, 1f));
            StartCoroutine(ChangeDistanceValue(distance, zoomIndistanceValue, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, zoomInRollAngleValue, 1f));

        }
        //zoom out
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, zoomOutrotAngleValue, 1f));
            StartCoroutine(ChangeDistanceValue(distance, zoomOutdistanceValue, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, zoomOutRollAngleValue, 1f));
        }
        //Back to orignal position
        if (cursor.GetComponent<CursorController>().isRunning == false)
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, 45.4f, 1f));
            StartCoroutine(ChangeDistanceValue(distance, 12.6f, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, -79.7f, 1f));
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

    void RotationController()
    {

        if (Input.GetKey("e"))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, rotRotR, 1f));
            StartCoroutine(ChangeDistanceValue(distance, rotDisR, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, rotRollR, 1f));
        }
        if (Input.GetKey("r"))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, rotRotL, 1f));
            StartCoroutine(ChangeDistanceValue(distance, rotDisL, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, rotRollL, 1f));
        }

    }

    IEnumerator ChangeRotAngleValue(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            rotAngle = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rotAngle = v_end;
    }
    IEnumerator ChangeDistanceValue(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            distance = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        distance = v_end;
    }
    IEnumerator ChangeRollAngleValue(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            rollAngle = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rollAngle = v_end;
    }

}