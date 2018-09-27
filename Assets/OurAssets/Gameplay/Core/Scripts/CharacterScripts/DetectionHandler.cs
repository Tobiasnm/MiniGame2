using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{

    public bool isDeadly = false;
    public bool isFarCollider = false;
    private EnemyHandler enemyHandler;

    void Start()
    {
        enemyHandler = GetComponentInParent<EnemyHandler>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isFarCollider)
        {
            enemyHandler.hasSeenPlayer = true;
            other.GetComponent<PlayerHandler>().HandleDetection(isDeadly);
            if (!isDeadly)
                enemyHandler.hasSeenPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isFarCollider)
        {
            enemyHandler.StopFollowingPlayer();


        }
    }
}
