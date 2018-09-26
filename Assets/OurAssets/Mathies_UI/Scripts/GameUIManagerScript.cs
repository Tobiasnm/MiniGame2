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
    private int curentPanelIndex;

    //Player variables
    private Vector2 direction;
    private GameObject player;

    //Hints Array
    private Text hintPanelText;
    public string[] hintTexts;
    private int currentShownHintIndex;
    private Text indexText;

	// Use this for initialization
	void Start () {
   
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
                default:
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
        hintPanelText.text = hintTexts[0];
        player = GameObject.FindGameObjectWithTag("Player");

        ResumeGame();
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
                pauseMenuUI.SetActive(true);
                hintUI.SetActive(false);
                settingsUI.SetActive(false);
                currentShownHintIndex = 0;
                hintPanelText.text = hintTexts[0];
                break;
            case 1:
                pauseMenuUI.SetActive(false);
                settingsUI.SetActive(true);
                break;
            case 2:
                pauseMenuUI.SetActive(false);
                hintUI.SetActive(true);
                break;
        }
    }

    public void PauseGame()
    {
        pauseButton.gameObject.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        hintUI.SetActive(false);
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
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
        SceneManager.LoadScene("MainMenu");
    }
}
