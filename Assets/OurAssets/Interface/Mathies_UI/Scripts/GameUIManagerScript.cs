using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUIManagerScript : MonoBehaviour {

    //Pause variables
    private Button pauseButton;
    private GameObject pauseMenuUI;
    private GameObject settingsUI;
    private GameObject hintUI;
    private GameObject walkieTalkieUI;
    private int curentPanelIndex;

    //Player variables
    private Vector2 direction;
    private GameObject player;

    //Hints Array
    private Text hintPanelText;
    public string[] hintTexts;
    private int currentShownHintIndex;
    private Text indexText;

    private Animator animator;

    private SubtitlesScript subtitlesScript;
    private SettingsScript settingsScript;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        subtitlesScript = GetComponent<SubtitlesScript>();
        settingsScript = GetComponent<SettingsScript>();
        GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("Pause");



        for (int i=0; i<pauseObjects.Length; i++)
        {
            switch (pauseObjects[i].name)
            {
                case "PauseButton":
                    pauseButton = pauseObjects[i].GetComponent<Button>();
                    break;
                case "SettingsPanel":
                    settingsUI = pauseObjects[i];
                    break;
                case "HintPanel":
                    hintUI = pauseObjects[i];
                    break;
                case "WalkieTalkieHint":
                    walkieTalkieUI = pauseObjects[i];
                    break;
                case "PausePanel":
                    pauseMenuUI = pauseObjects[i];
                    break;
            }
        }
        for (int i=0; i < hintUI.transform.childCount; i++)
        {
            switch (hintUI.transform.GetChild(i).name)
            {
                case "HintText":
                    hintPanelText = hintUI.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "IndexText":
                    indexText = hintUI.transform.GetChild(i).GetComponent<Text>();
                    break;
            }
        }

        for (int i = 0; i < hintUI.transform.childCount; i++)
        {
            switch (hintUI.transform.GetChild(i).name)
            {
                case "HintText":
                    hintPanelText = hintUI.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "IndexText":
                    indexText = hintUI.transform.GetChild(i).GetComponent<Text>();
                    break;
            }
        }
        
        currentShownHintIndex = 0;
        if (hintTexts != null && hintTexts.Length > 0) hintPanelText.text = hintTexts[0];
        player = GameObject.FindGameObjectWithTag("Player");

        hintUI.SetActive(false);
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update() {
        if (hintTexts == null)
            return;

        indexText.text = (currentShownHintIndex + 1) + " / " + hintTexts.Length;
    }

    public void SetPanelActive(int panel)
    {
        switch (panel)
        {
            case 0:
                hintUI.SetActive(false);
                settingsUI.SetActive(false);
                SetChildrenActive(0, true);
                SetChildrenActive(1, false);
                currentShownHintIndex = 0;
                hintPanelText.text = hintTexts[0];
                break;
            case 1:
                SetChildrenActive(0, false);
                SetChildrenActive(1, false);
                settingsUI.SetActive(true);
                if (settingsScript.GetLanguageButton() == null) settingsScript.SetLanguageButton(GameObject.FindGameObjectWithTag("LanguageButton"));
                break;
            case 2:
                hintUI.SetActive(true);
                walkieTalkieUI.SetActive(false);
                SetChildrenActive(0, false);
                break;
        }
    }

    private void SetChildrenActive(int index, bool active)
    {
        GameObject[] go = { pauseMenuUI, walkieTalkieUI };
        foreach (Transform child in go[index].transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    public void PauseGame()
    {
        AkSoundEngine.SetState("Pause_Menu_Filter", "Pause_On");
        SetChildrenActive(0, true);
        if (subtitlesScript.startAnimation) subtitlesScript.ResetSubtitles();
        animator.SetTrigger("pause_fade");
    }

    public void PauseComplete()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        SetChildrenActive(1, false);
        Time.timeScale = 1f;
        AkSoundEngine.SetState("Pause_Menu_Filter", "Pause_Off");
        animator.SetTrigger("pause_disable");
    }

    public void PrevHint()
    {
        currentShownHintIndex--;
        if (currentShownHintIndex >= 0)
            hintPanelText.text = hintTexts[currentShownHintIndex];
        else
        {
            currentShownHintIndex = 0;
            SetPanelActive(0);
        }
            
    }

    public void NextHint()
    {
        currentShownHintIndex++;
        if (currentShownHintIndex < hintTexts.Length)
            hintPanelText.text = hintTexts[currentShownHintIndex];
        else
        {
            currentShownHintIndex = 0;
            hintPanelText.text = hintTexts[currentShownHintIndex];
            SetPanelActive(0);
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void DoWalkieTalkie()
    {
        string[] text = { "something, something", "aasss", "ass" };
        float[] duration = { 6f, 5f, 6f, };
        ShowWalkieTalkieText(text, duration);
    }

    //ShowWalkieTalkieText("some text here", 3);string text, float duration
    public void ShowWalkieTalkieText(string[] text, float[] duration)
    {

        subtitlesScript.subtitles = text;
        subtitlesScript.timeLimits = duration;
        SetChildrenActive(1, true);
        SetChildrenActive(0, false);
        subtitlesScript.startAnimation = true;
        animator.SetTrigger("enter_subtitle");
    }

    public void RemoveWalkieTalkie()
    {
        //SetChildrenActive(1, false);
        subtitlesScript.ResetSubtitles();
    }
}
