using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUIManagerScriptAlt : MonoBehaviour
{

    //Pause variables
    private Button pauseButton;
    private GameObject pauseMenuUI;
    private GameObject settingsUI;
    private int curentPanelIndex;

	// Use this for initialization
	void Start () {
        GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("Pause");


        pauseMenuUI = pauseObjects[2];        
        pauseButton = pauseObjects[0].GetComponent<Button>();
        settingsUI = pauseObjects[1];

        ResumeGame();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void SetPanelActive(int panel)
    {
        switch (panel)
        {
            case 0:
                pauseMenuUI.SetActive(true);
                settingsUI.SetActive(false);
                break;
            case 1:
                pauseMenuUI.SetActive(false);
                settingsUI.SetActive(true);
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
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
