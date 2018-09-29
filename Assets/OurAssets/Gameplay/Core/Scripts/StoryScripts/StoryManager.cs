using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private TriggerStory activeStory;
    //public string textToShow ="";
    //private UIController uiController;
    [HideInInspector]
    public PlayerHandler playerHandler;
    private Queue<Sentence> conversation = new Queue<Sentence>();

    //public LevelChangerScript levelChanger;
    //public string nextLevelName;

    //public bool hasWon { get; set; } = false;

    private bool conversationLockEnabled = false;

    public void AddStory(TriggerStory story)
    {
        activeStory = story;
        conversation = new Queue<Sentence>(activeStory.conversation);
    }

    // Use this for initialization
    void Start()
    {
        //uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    public void ShowText(Sentence line)
    {
        //uiController.FillTextField(text);
        //uiController.ShowTextArea();
        if (line.audioClipName != "")
            AkSoundEngine.PostEvent(line.audioClipName, gameObject);

        Debug.Log("Audio name: " + line.audioClipName + " Duration: " + line.conversationLength + " Text: " + line.text);
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
        /*
        if (uiController.GetTextEmpty() && textList.Count > 0)
        {
            ShowText(textList.Dequeue());
            playerHandler.StopLocomotion();
            //Debug.Log("Stopped Locomotion."+ textList.Count);
        }
        else if (uiController.GetTextEmpty() && textList.Count == 0)
        {
            if (hasWon)
            {
                levelChanger.FadeToLevel(nextLevelName);
                PlayerPrefs.SetString("reached_level", nextLevelName);
            }
            else
            {
                playerHandler.StartLocomotion();
                //Debug.Log("Started Locomotion");
            }
        }
        */

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
