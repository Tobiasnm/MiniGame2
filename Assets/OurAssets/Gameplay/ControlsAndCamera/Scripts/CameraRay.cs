using UnityEngine;
using System.Collections;

public class CameraRay : MonoBehaviour
{
    private Transform player;
    private Vector3 offset; 
    private float distance;
    private Vector3[] points = new Vector3[5];

    private Vector3 targetPos;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
        distance = offset.magnitude;
    }

    void FixedUpdate()
    {
        points[0] = player.position + offset*1.5f;
        points[4] = player.position + Vector3.up * distance;

        points[1] = Vector3.Lerp(points[0], points[4], 0.25f);
        points[2] = Vector3.Lerp(points[0], points[4], 0.5f);
        points[3] = Vector3.Lerp(points[0], points[4], 0.75f);
        points[4] = Vector3.Lerp(points[0], points[4], 0.9f);

        targetPos = FindCameraTarget();

        AdjustCamera();
    }

    private Vector3 FindCameraTarget()
    {
        Vector3 result = points[points.Length - 1];

       
        for (int i = 0; i < points.Length; ++i)
        {
            if (IsHitPlayer(points[i], player.position))
            {
                result = points[i];
                break;
            }
        }

        return result;
    }

    private Ray ray;
    private RaycastHit hit;

    bool IsHitPlayer(Vector3 origin, Vector3 target)
    {
        bool result = false;

        Vector3 dir = target - origin;
        ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                result = true;
            }
        }
        return result;
    }


    void AdjustCamera()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20f);
    }
}