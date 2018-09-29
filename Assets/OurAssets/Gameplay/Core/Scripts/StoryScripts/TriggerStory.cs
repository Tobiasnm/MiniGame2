using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerStory : MonoBehaviour
{

    //private string storyText = "";
    [Header("Toogle and fill if this should cause rain")]
    public bool causeRain = false;
    public float startRainInSeconds = 10;
    public float doThunderInSeconds = 10;

    [Header("Story-related parameters")]
    public bool isWinCondition = false;
    public bool pauseToListen = false;

    public List<Sentence> conversation = new List<Sentence>();

    //public Transform focusTarget;
    //public float stopLookingInSeconds = 5;

    public string nextLevelName;

    private Collider storyCollider;
    private StoryManager storyManager;
    private AkEvent audioSource;
    private CameraHandler cameraHandler;
    private HandleRain rainHandler;

    private GameUIManagerScript uiManager;


    void Start()
    {
        storyCollider = GetComponent<Collider>();

        storyManager = GameObject.FindGameObjectWithTag("StoryManager").GetComponent<StoryManager>();        

        audioSource = GetComponent<AkEvent>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        rainHandler = GameObject.FindGameObjectWithTag("RainManager").GetComponent<HandleRain>();
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            storyManager.AddStory(this);

            if (causeRain)
            {
                rainHandler.StartRainInSeconds(startRainInSeconds, doThunderInSeconds);
            }
            
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    
}
