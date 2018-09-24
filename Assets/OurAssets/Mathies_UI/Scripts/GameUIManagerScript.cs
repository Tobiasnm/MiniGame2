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

	// Use this for initialization
	void Start () {
        joystickBg = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Image>(true);
        joystickFg = joystickBg.transform.GetChild(0).GetComponent<Image>();
        bgImgColor = joystickBg.color;
        fgImgColor = joystickFg.color;

        GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
        pauseMenuUI = pauseObjects[0];
        pauseButton = pauseObjects[1].GetComponent<Button>();

        pauseMenuUI.SetActive(false);

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
                        joystickFg.transform.position = new Vector2(joystickBg.transform.position.x, joystickBg.transform.position.y) + (touch.position - startPos).normalized * 10;
                    }
                break;
                    case TouchPhase.Ended:
                    touchStart = false;
                break;
            }
        }

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
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
