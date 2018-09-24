using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUIManagerScript : MonoBehaviour {

    //Joystick variables
    private Image joystickBg;
    private Image joystickFg;
    private Color bgImgColor;
    private Color fgImgColor;
    private bool touchStart = false;
    private Vector2 startPos;

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
        joystickBg = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Image>(true);
        joystickFg = joystickBg.transform.GetChild(0).GetComponent<Image>();
        bgImgColor = joystickBg.color;
        fgImgColor = joystickFg.color;

        GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
        pauseMenuUI = pauseObjects[2];        
        pauseButton = pauseObjects[0].GetComponent<Button>();
        settingsUI = pauseObjects[1];

        player = GameObject.FindGameObjectWithTag("Player");

        ResumeGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
                {
                    case TouchPhase.Began:
                    if (!IsPointerOverUIObject())
                    startPos = touch.position;
                break;
                    case TouchPhase.Moved:
                    if (!IsPointerOverUIObject())
                    {
                        touchStart = true;
                        joystickBg.transform.position = startPos;
                        joystickFg.transform.position = new Vector2(joystickBg.transform.position.x, joystickBg.transform.position.y) + (touch.position - startPos).normalized * 70;
                        direction = (touch.position - startPos) / (touch.position - startPos).magnitude;
                    }
                break;
                    case TouchPhase.Ended:
                    touchStart = false;
                break;
            }
        }

        if (direction != null) player.GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 0f, direction.y) * 50f);

        if (touchStart)
        {
            joystickBg.color = bgImgColor;
            joystickFg.color = fgImgColor;
        }
        else
        {
            joystickBg.color = new Color(0, 0, 0, 0);
            joystickFg.color = new Color(0, 0, 0, 0);
        }
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
