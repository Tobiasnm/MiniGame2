using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class EnemyHandler : MonoBehaviour
{

    public Transform[] points;
    public Transform player;
    public bool hasSeenPlayer = false;
    public float barkDetectionLength = 5;

    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private Outline enemyOutline;
    private PlayerHandler playerHandler;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        playerHandler = player.gameObject.GetComponent<PlayerHandler>();

        //Find and initialize the outline
        enemyOutline = GetComponentInChildren<Outline>();
        enemyOutline.enabled = false;

    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void FollowPlayer()
    {
        agent.destination = player.position;
        OpenEnemyOutline();
    }

    ////TODO: turn into an event driven structure
    //private void ListenForPlayer()
    //{
    //    //listen for the changes in the hasDetected variable

    //    hasHeardPlayer = playerHandler.isBarking;
    //}

    private void CloseEnemyOutline()
    {
        enemyOutline.enabled = false;

    }


    private void OpenEnemyOutline()
    {
        enemyOutline.enabled = true;

    }

    public void StopFollowingPlayer()
    {
        CloseEnemyOutline();
        hasSeenPlayer = false;
        GotoNextPoint();
    }

    //TODO: Fix clunky walk aniamtion

    void Update()
    {
        //ListenForPlayer();

        //if the player is detected via sound or sight, follow forever
        if (hasSeenPlayer)
        {
            FollowPlayer();
        }
        //if the player is not detected, patrol
        else if (!agent.pathPending && agent.remainingDistance < 0.5f && !hasSeenPlayer)
            GotoNextPoint();
        else
        {
            //do nothing but walk to the current point
        }

    }
}
