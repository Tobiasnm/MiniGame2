using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManagerScript : MonoBehaviour {

    private GameObject[] menuPanels;
    private int currentActivePanel;

	// Use this for initialization
	void Start () {
        menuPanels = GameObject.FindGameObjectsWithTag("MenuPanel");

        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false);
        }
        //Make the Main Menu-panel the last panel in the list so index is length-1.
        currentActivePanel = menuPanels.Length-1;
        menuPanels[2].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetPanelActive(int panelIndex)
    {
        menuPanels[currentActivePanel].SetActive(false);
        menuPanels[panelIndex].SetActive(true);
        currentActivePanel = panelIndex;
    }

    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
