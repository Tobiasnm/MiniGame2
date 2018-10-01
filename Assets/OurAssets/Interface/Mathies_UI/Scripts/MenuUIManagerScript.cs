using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuUIManagerScript : MonoBehaviour {

    private GameObject menuPanel;
    private GameObject chaptersPanel;
    private GameObject settingsPanel;
    private GameObject creditsPanel;
    private int currentActivePanel;

    public string[] levelNames;

	// Use this for initialization
	void Start () {
        GameObject[] menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");

        for (int i = 0; i < menuPanels.Length; i++)
        {
            switch (menuPanels[i].name)
            {
                case "ChaptersPanel":
                    chaptersPanel = menuPanels[i];
                    break;
                case "SettingsPanel":
                    settingsPanel = menuPanels[i];
                    break;
                case "CreditsPanel":
                    creditsPanel = menuPanels[i];
                    break;
                case "MenuPanel":
                    menuPanel = menuPanels[i];
                    break;
            }
        }

        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);
        GameObject[] chapterButtons = GameObject.FindGameObjectsWithTag("ChapterButton");

        chapterButtons[0].GetComponent<Button>().interactable = false;
        chapterButtons[1].GetComponent<Button>().interactable = false;

        switch (reachedLevel)
        {
            case 2:
                chapterButtons[0].GetComponent<Button>().GetComponentInChildren<Text>().text = levelNames[0];
                chapterButtons[0].GetComponent<Button>().interactable = true;

                break;
            case 3:
                chapterButtons[0].GetComponent<Button>().transform.GetChild(1).GetComponent<Text>().text = levelNames[0];
                chapterButtons[1].GetComponent<Button>().transform.GetChild(1).GetComponent<Text>().text = levelNames[1];
                chapterButtons[0].GetComponent<Button>().interactable = true;
                chapterButtons[1].GetComponent<Button>().interactable = true;
                break;
        }

        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false);
        }
        menuPanel.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetPanelActive(int panel)
    {
        menuPanel.SetActive(false);
        chaptersPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        switch (panel)
        {
            case 0:
                menuPanel.SetActive(true);
                break;
            case 1:
                chaptersPanel.SetActive(true);
                break;
            case 2:
                settingsPanel.SetActive(true);
                break;
            case 3:
                creditsPanel.SetActive(true);
                break;
        }
    }

    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLevel(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

 
}
