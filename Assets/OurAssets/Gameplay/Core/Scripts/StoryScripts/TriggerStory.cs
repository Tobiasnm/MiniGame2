using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStory : MonoBehaviour
{


    //private string storyText = "";
    [Header("Toogle and fill if this should cause rain.")]
    public bool causeRain = false;
    public float doSoundInSeconds = 1;
    public float startRainInSeconds = 10;
    [Header("...")]

    public bool isWinCondition = false;
    public Transform focusTarget;
    public float stopLookingInSeconds = 5;
    public List<string> story = new List<string>();
    public string nextLevelName;

    private Collider storyCollider;
    private StoryManager storyManager;
    private AkEvent audioSource;
    private CameraHandler cameraHandler;
    private HandleRain rainHandler;
    public GameObject rain;


    // Use this for initialization
    void Start()
    {
        storyCollider = GetComponent<Collider>();
        storyManager = GetComponentInParent<StoryManager>();
        audioSource = GetComponent<AkEvent>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        rainHandler = GameObject.FindGameObjectWithTag("RainHandler").GetComponent<HandleRain>();
        rain = GameObject.FindGameObjectWithTag("TheRain");
    }

    private void DoSound()
    {
        AkSoundEngine.PostEvent("Thunderstrike", rain);
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            storyManager.nextLevelName = nextLevelName;
            storyManager.HasWon = isWinCondition;

            storyManager.textList = new Queue<string>(story);
            if (causeRain)
            {
                DoSoundInSeconds(doSoundInSeconds);
                rainHandler.startRainInSeconds(startRainInSeconds);

            }

            if (focusTarget)
                cameraHandler.target = focusTarget;
            Destroy(this.gameObject);
        }
    }

    private void DoSoundInSeconds(float seconds)
    {

        Invoke("DoSound", seconds);
    }
}
