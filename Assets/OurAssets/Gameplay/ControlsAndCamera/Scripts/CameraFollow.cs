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
    private Vector3 targetPos;

    public float zoomInrotAngleValue = 20f;
    public float zoomIndistanceValue = 5f;
    public float zoomInRollAngleValue = -88.2f;
    public float zoomOutrotAngleValue = 50f;
    public float zoomOutdistanceValue = 20f;
    public float zoomOutRollAngleValue = 20f;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        UpdatePosition();
        Zooming();
    }

    public void UpdatePosition()
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
        //zoom in
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, zoomInrotAngleValue, 1f));
            StartCoroutine(ChangeDistanceValue(distance, zoomIndistanceValue, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle,zoomInRollAngleValue, 1f));
            Debug.Log("zoomIn");
        }
        //zoom out
        if(Input.GetMouseButton(1))
        {
            StartCoroutine(ChangeRotAngleValue(rotAngle, zoomOutrotAngleValue, 1f));
            StartCoroutine(ChangeDistanceValue(distance, zoomOutdistanceValue, 1f));
            StartCoroutine(ChangeRollAngleValue(rollAngle, zoomOutRollAngleValue, 1f));
        }
        if(Input.GetMouseButton(2))
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
