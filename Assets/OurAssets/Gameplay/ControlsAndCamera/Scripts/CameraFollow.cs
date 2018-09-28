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
    private float rot;//YZ平
    private Vector3 targetPos;
    public float ZoomInrotAngleValue = 20f;
    public float ZoomOutrotAngleValue = 5f;
    public float ZoomIndistanceValue = 50f;
    public float ZoomOutdistanceValue = 20f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        UpdatePosition(); 
        Zooming();
    }

    private void FixedUpdate()
    {

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

     void Zooming()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    transform.position = Vector3.Lerp(transform.position, ZoomIn, Time.deltaTime * 3);
        //    Debug.Log("zoomIn");
        //}

        //if(Input.GetMouseButton(1))
        //{
        //    transform.position = Vector3.Lerp(transform.position, ZoomOut, Time.deltaTime * 3);
        //    Debug.Log("zoomOut");
        //}
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, 20f, 1f));
            StartCoroutine(ChangeDistanceValue(distance, 5f, 1f));
            Debug.Log("zoomIn");
        }
        if(Input.GetMouseButton(1))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, 50f, 1f));
            StartCoroutine(ChangeDistanceValue(distance, 20f, 1f));
        }
        if(Input.GetMouseButton(2))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, 45.4f, 1f));
            StartCoroutine(ChangeDistanceValue(distance, 12.6f, 1f));

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
}