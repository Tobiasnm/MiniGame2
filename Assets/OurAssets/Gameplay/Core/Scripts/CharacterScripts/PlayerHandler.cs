﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public float distanceToTap = 100;


    private int maxHealth = 100;
    //private HandleGirl girlHandler;
    private bool hasPickedFood = false;
    private GameObject food;
    private Camera mainCamera;
    private bool canHowl = false;
    private bool doHowl = false;

    //private float storedAgentSpeed;

    public void StopLocomotion()
    {
        //playerController.CanMove = false;
        //playerController.Agent.isStopped = true;
        //playerController.Agent.SetDestination(playerController.transform.position);
    }

    public void StartLocomotion()
    {
        //playerController.CanMove = true;
        //playerController.Agent.isStopped = false;
    }


    public void RemoveHealth(int amount)
    {
        maxHealth -= amount;
    }

    public void AddHealth(int amount)
    {
        maxHealth += amount;
    }

    public int GetHealth(int amount)
    {
        return maxHealth;
    }

    public void HandleDeath()
    {
        if (maxHealth <= 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

    }

    public void HandleDetection(bool isDeadly)
    {
        if (isDeadly)
            Debug.Log("You are dead. Fuckin casual..");
        //SceneManager.LoadScene("GameOver", LoadSceneMode.Single);


    }


    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        //girlHandler = littleGirl.GetComponent<HandleGirl>();
        //playerController = GameObject.FindGameObjectWithTag("Cursor").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleDeath();


#if UNITY_STANDALONE
        //if (Input.GetKeyDown("h"))
        //{
        //    Bark();
        //    Debug.Log("I am barking");
        //}

        if (Input.GetKeyDown("j") && hasPickedFood)
        {
            PlaceItemOnGround();
        }
#endif

    }

    private void FixedUpdate()
    {
        DoTapCalculations();
    }


    //Handle collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            PickUpItem(other.gameObject);
            Debug.Log("Picked up food");
        }
    }

    private void PlaceItemOnGround()
    {
        food.transform.position = this.transform.position;
        food.GetComponent<BoxCollider>().enabled = false;
        food.SetActive(true);
        hasPickedFood = false;
    }

    private void PickUpItem(GameObject other)
    {
        hasPickedFood = true;
        food = other;
        food.SetActive(false);
    }


    void DoTapCalculations()
    {


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    CheckTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
        }
    }

    private void CheckTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        Debug.Log("Click");

        if (Physics.Raycast(ray, out hit, 100000))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Checking distance");
                if (Vector3.Distance(this.transform.position, hit.transform.position) < distanceToTap)
                    hit.transform.gameObject.GetComponent<ObstacleHandler>().RegisterTap();
            }
        }

    }

}
