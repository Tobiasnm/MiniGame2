using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public bool isBarking = false;
    public float barkLength = 5;
    public float girlShoutsInSecondsAfterBark = 1;

    private int maxHealth = 100;
    //private HandleGirl girlHandler;
    private bool hasPickedFood = false;
    private GameObject food;
    private Camera mainCamera;
    private bool canHowl = false;
    private bool doHowl = false;
    private PlayerController playerController;
    //private float storedAgentSpeed;

    public void StopLocomotion()
    {
        playerController.CanMove = false;
        playerController.Agent.isStopped = true;
        //playerController.Agent.SetDestination(playerController.transform.position);
    }

    public void StartLocomotion()
    {
        playerController.CanMove = true;
        playerController.Agent.isStopped = false;
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


    private void MakeGirlShout()
    {
        Debug.Log("Girl shouts!!!");
        //girlHandler.GirlScream();
    }


    //public void Bark()
    //{
    //    if (isBarking)
    //        return;
    //    isBarking = true;
    //    AkSoundEngine.PostEvent("Bark", gameObject);
    //    Invoke("MakeGirlShout", girlShoutsInSecondsAfterBark);
    //    Invoke("StopBarking", barkLength);
    //    this.GetComponent<Animator>().SetTrigger("Howl");
    //    mainCamera.GetComponent<CameraHandler>().target = littleGirl.transform;
    //}

    public void StopBarking()
    {
        //marks the barking sound as stopped for the rest of the scripts. Does not affect sound.
        isBarking = false;

    }

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        //girlHandler = littleGirl.GetComponent<HandleGirl>();
        playerController = GameObject.FindGameObjectWithTag("Cursor").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleDeath();


        //#if UNITY_ANDROID
        //        //mobile input
        //        // Track a single touch as a direction control.
        //        if (Input.touchCount > 0)
        //        {
        //            Touch touch = Input.GetTouch(0);
        //            // Handle finger movements based on TouchPhase
        //            switch (touch.phase)
        //            {
        //                //When a touch has first been detected, change the message and record the starting position
        //                case TouchPhase.Began:
        //                    // Record initial touch position.

        //                    if(canHowl)
        //                    {
        //                        doHowl = true;
        //                    }
        //                    else
        //                        doHowl = false;
        //                        canHowl = true;
        //                    break;

        //                case TouchPhase.Ended:
        //                    if(doHowl)
        //                    {
        //                        Bark();
        //                        Debug.Log("I am barking");
        //                        canHowl = false;
        //                        doHowl = false;
        //                    }
        //                    break;
        //            }
        //        }
        //#endif

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
}
