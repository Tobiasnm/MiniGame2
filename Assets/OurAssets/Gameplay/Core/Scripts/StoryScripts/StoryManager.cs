using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private TriggerStory activeStory;

    [HideInInspector]
    public PlayerHandler playerHandler;
    private Queue<Sentence> conversation = new Queue<Sentence>();

    private bool conversationLockEnabled = false;

    private GameUIManagerScript gUIManager;
    //private SubtitlesScript subtitlesManager = GameObject.FindGameObjectWithTag("GUICanvas").GetComponent<SubtitlesScript>();

    public void AddStory(TriggerStory story)
    {
        activeStory = story;
        conversation = new Queue<Sentence>(activeStory.conversation);
    }

    void Start()
    {
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        gUIManager = GameObject.FindGameObjectWithTag("GUICanvas").GetComponent<GameUIManagerScript>();
    }

    public void ShowText(Sentence line)
    {
        if (line.audioClipName != "")
            AkSoundEngine.PostEvent(line.audioClipName, gameObject);
        string[] tempConv = new string[1];
        tempConv[0] = line.text;
        float[] tempDur = new float[1];
        tempDur[0] = line.conversationLength;

        gUIManager.ShowWalkieTalkieText(tempConv, tempDur);

    }

    void FixedUpdate()
    {
        HandlePlayerPause();
        ShowStory();
    }

    private void HandlePlayerPause()
    {
        if (activeStory == null || !activeStory.pauseToListen)
            return;
        if (conversationLockEnabled)
            playerHandler.StopLocomotion();
    }

    public void ShowStory()
    {

        if (!conversationLockEnabled && conversation.Count > 0)
        {
            StartCoroutine(PerformConversation());
            conversationLockEnabled = true;
        }
    }

    IEnumerator PerformConversation()
    {
        while (conversation.Count > 0)
        {
            Sentence line = conversation.Dequeue();
            ShowText(line);
            yield return new WaitForSecondsRealtime(line.conversationLength);
        }
        conversationLockEnabled = false;
        playerHandler.StartLocomotion();
    }

}
