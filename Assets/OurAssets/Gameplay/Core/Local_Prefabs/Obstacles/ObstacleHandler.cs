using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    //[Range(0,1)]
    //public float triggerEventAtPercentage = 0.0f;
    [Header("Events need to be in order from first to last")]
    public MusicEvent[] events;
    private Queue<MusicEvent> eventQueue = new Queue<MusicEvent>();
    private MusicEvent nextEvent;
    MusicEvent latestEvent;
    private Queue<GameObject> obstacles = new Queue<GameObject>();

    [SerializeField]
    int tapsToBreak = 10;

    [SerializeField]
    int tapsDone = 0;

    [SerializeField]
    string tapSound;

    private GameObject ambienceSoundBank;

    // Use this for initialization
    void Start()
    {
        ambienceSoundBank = GameObject.FindGameObjectWithTag("AmbienceSoundBank");

        MeshRenderer[] renderedChildren = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in renderedChildren)
        {
            obstacles.Enqueue(child.gameObject);
            Debug.Log(child.name);
        }
        if (events.Length <= 0)
            eventQueue = new Queue<MusicEvent>();
        else
            eventQueue = new Queue<MusicEvent>(events);
        nextEvent = eventQueue.Dequeue();
    }

    public void RegisterTap()
    {
        tapsDone += 1;
        AkSoundEngine.PostEvent(tapSound, gameObject);
        this.GetComponentInChildren<ParticleSystem>().Emit(Mathf.CeilToInt(200 / tapsToBreak));
        Debug.Log("Tap");

        latestEvent = CheckForEvents();
        if (latestEvent != null)
            TriggerEvent(latestEvent);
    }

    private MusicEvent CheckForEvents()
    {
        if (nextEvent.triggerEventAtPercentage >= tapsDone / tapsToBreak)
            return nextEvent;
        else
            return null;

    }

    private void DestroyObstacle()
    {
        //Do before destruction
        if (obstacles.Count > 0)
        {
            Destroy(obstacles.Dequeue());
        }
        if (obstacles.Count <= 0)
        {
            Destroy(this.gameObject);
            AkSoundEngine.SetRTPCValue(nextEvent.audioClipName, 0.5f, ambienceSoundBank, 1000);
        }
    }



    private void TriggerEvent(MusicEvent anEvent)
    {
        AkSoundEngine.SetRTPCValue(anEvent.audioClipName, anEvent.soundLevel, ambienceSoundBank, 1000);

        //add sound call and event behavior
        if (eventQueue.Count > 0)
            nextEvent = eventQueue.Dequeue();

    }

    // Update is called once per frame
    void Update()
    {
        if (obstacles.Count > 0 && tapsDone * obstacles.Count >= tapsToBreak)
        {
            DestroyObstacle();
        }
    }


}

