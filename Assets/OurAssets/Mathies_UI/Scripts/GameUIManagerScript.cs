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
    private int curentPanelIndex;

    //Player variables
    private Vector2 direction;
    private GameObject player;

	// Use this for initialization
	void Start () {
   
        GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
        for (int i=0; i<pauseObjects.Length; i++)
        {
            if (pauseObjects[i].name == "PauseButton") pauseButton = pauseObjects[i].GetComponent<Button>();
            else if (pauseObjects[i].name == "SettingsPanel") settingsUI = pauseObjects[i];
            else pauseMenuUI = pauseObjects[i];
        }
        player = GameObject.FindGameObjectWithTag("Player");

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

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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
