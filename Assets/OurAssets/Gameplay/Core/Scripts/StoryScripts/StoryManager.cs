using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{

    //public string textToShow ="";
    //private UIController uiController;
    [HideInInspector]
    public PlayerHandler playerHandler;
    public Queue<string> textList = new Queue<string>();
    //public LevelChangerScript levelChanger;
    [HideInInspector]
    public string nextLevelName;

    public bool hasWon { get; set; } = false;

    // Use this for initialization
    void Start()
    {
        //uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    public void ShowText(string text)
    {
        //uiController.FillTextField(text);
        //uiController.ShowTextArea();
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
