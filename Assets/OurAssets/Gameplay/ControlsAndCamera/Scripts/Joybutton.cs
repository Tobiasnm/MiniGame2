﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IPointerClickHandler {

    [HideInInspector]
    public bool Pressed;
    public bool clicked;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
    }
}