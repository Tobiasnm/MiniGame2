using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{

    public bool isDeadly = false;
    public bool isFarCollider = false;
    private EnemyHandler enemyHandler;
    private Transform player;
    private int playerMask = 1 << 13;
    private int nonPlayerMask;
    Vector3 normalizedDirectionToPlayer;
    float distanceToPlayer;
    void Start()
    {
        enemyHandler = GetComponentInParent<EnemyHandler>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nonPlayerMask = ~(playerMask | 1 << 2); 
    }


    private bool IsPlayerDetected()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        distanceToPlayer = (player.position - this.transform.position).magnitude;
        normalizedDirectionToPlayer = (player.position - this.transform.position)/ distanceToPlayer;

        //shoot a ray that cannot hit the player, up to the position of the player
        if (Physics.Raycast(transform.position, normalizedDirectionToPlayer, out hit, distanceToPlayer,nonPlayerMask))
        {
            Debug.Log(hit.transform.tag);
            Debug.DrawRay(transform.position, normalizedDirectionToPlayer * distanceToPlayer, Color.white);
            Debug.Log("Did Not hit Player");
            //return false if something is in the way
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, normalizedDirectionToPlayer * distanceToPlayer, Color.red);
            Debug.Log("Did Hit player");
            return true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isFarCollider)
        {
            if (isDeadly)
                other.GetComponent<PlayerHandler>().HandleDetection(isDeadly);

            if (IsPlayerDetected())
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
