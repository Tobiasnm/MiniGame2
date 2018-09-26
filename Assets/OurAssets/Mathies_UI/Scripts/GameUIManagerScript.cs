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

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
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
        currentShownHintIndex = 0;
        if (hintTexts != null && hintTexts.Length > 0) hintPanelText.text = hintTexts[0];
        player = GameObject.FindGameObjectWithTag("Player");

        hintUI.SetActive(false);
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        walkieTalkieUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        indexText.text = (currentShownHintIndex + 1) + " / " + hintTexts.Length;
    }

    public void SetPanelActive(int panel)
    {
        switch (panel)
        {
            case 0:
                hintUI.SetActive(false);
                settingsUI.SetActive(false);
                SetChildrenActive(true);
                currentShownHintIndex = 0;
                hintPanelText.text = hintTexts[0];
                break;
            case 1:
                SetChildrenActive(false);
                settingsUI.SetActive(true);
                break;
            case 2:
                hintUI.SetActive(true);
                SetChildrenActive(false);
                break;
        }
    }

    private void SetChildrenActive(bool active)
    {
        foreach (Transform child in pauseMenuUI.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    public void PauseGame()
    {
        animator.SetTrigger("pause_fade");
    }

    public void PauseComplete()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("pause_fade");
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

    public void SetWalkieTalkie(int hintIndex)
    {
        walkieTalkieUI.SetActive(true);
        walkieTalkieUI.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = hintTexts[hintIndex];
    }

    public void RemoveWalkieTalkie()
    {
        walkieTalkieUI.SetActive(false);
    }
}
