using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private List<TriggerStory> stories = new List<TriggerStory>();
    //public string textToShow ="";
    //private UIController uiController;
    [HideInInspector]
    public PlayerHandler playerHandler;
    public Queue<string> textList = new Queue<string>();

    //public LevelChangerScript levelChanger;
    [HideInInspector]
    public string nextLevelName;

    public bool hasWon { get; set; } = false;

    public void AddStory(TriggerStory story)
    {
        stories.Add(story);
    }

    // Use this for initialization
    void Start()
    {
        //uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    public void ShowText(string text,string sound)
    {
        //uiController.FillTextField(text);
        //uiController.ShowTextArea();
        AkSoundEngine.PostEvent(sound, gameObject);
        Debug.Log("sound; "+sound+" text; "+text);

    }

    void FixedUpdate()
    {
        ShowStory();
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
    }
}
