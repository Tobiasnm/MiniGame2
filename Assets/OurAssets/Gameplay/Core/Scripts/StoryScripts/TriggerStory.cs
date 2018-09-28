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
    //public Transform focusTarget;
    //public float stopLookingInSeconds = 5;
    public List<string> story = new List<string>();
    [Header("Order matters. First in-first played")]
    public List<string> audioSources = new List<string>();
    private Queue<string> audioSourcesQueue;

    public string nextLevelName;

    private Collider storyCollider;
    private StoryManager storyManager;
    private AkEvent audioSource;
    private CameraHandler cameraHandler;
    private HandleRain rainHandler;

    public GameUIManagerScript uiManager;


    void Start()
    {
        storyCollider = GetComponent<Collider>();

        storyManager = GameObject.FindGameObjectWithTag("StoryManager").GetComponent<StoryManager>();
        //add this story to the list of stories
        storyManager.AddStory(this);

        audioSource = GetComponent<AkEvent>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        rainHandler = GameObject.FindGameObjectWithTag("RainManager").GetComponent<HandleRain>();

        audioSourcesQueue = new Queue<string>(audioSources);
    }
    

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //storyManager.nextLevelName = nextLevelName;
            storyManager.hasWon = isWinCondition;

            storyManager.textList = new Queue<string>(story);

            if (causeRain)
            {                
                rainHandler.StartRainInSeconds(startRainInSeconds, doThunderInSeconds);
            }

            if (uiManager != null) uiManager.ShowWalkieTalkieText();
            //if (focusTarget)
            //    cameraHandler.target = focusTarget;
            StartCoroutine(TellStories());

            Destroy(this.gameObject);
        }
    }

    IEnumerator TellStories()
    {
        foreach (var text in story)
            if (audioSourcesQueue.Count > 0)
            {
                storyManager.ShowText(text, audioSourcesQueue.Dequeue());
                yield return new WaitForSecondsRealtime(5);
            }
        
    }
}
