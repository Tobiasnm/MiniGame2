using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SubtitlesScript : MonoBehaviour {

    public string[] subtitles;
    private int subIndex;
    private float timer;
    private Animator animator;

    private GameObject subOne;
    private GameObject subTwo;

    public float[] timeLimits;

    public bool startAnimation = false;
    public bool gameplayScene = false;

    private Vector3 signePosition;
    private Vector3 rubenPosition;

	// Use this for initialization
	void Start () {
        subIndex = 0;
        timer = 0;
        signePosition = new Vector3(-200, 300, 0);
        rubenPosition = new Vector3(200, -160, 0);
        animator = GetComponent<Animator>();
        GameObject[] subObjects = GameObject.FindGameObjectsWithTag("Language");
        for (int i = 0; i < subObjects.Length; i++) {
            if (subObjects[i] != null && subObjects[i].name == "SubOne") subOne = subObjects[i];
            if (subObjects[i] != null && subObjects[i].name == "SubTwo") subTwo = subObjects[i];
        }

        if (subOne != null) subOne.GetComponent<Text>().text = subtitles[subIndex];
    }
	
	// Update is called once per frame
	void Update () {
        if (startAnimation)
        {
            if (timer >= timeLimits[subIndex])
            {
                timer = 0f;
                NextSubtitle();
            }
            timer += Time.deltaTime;
        }
	}

    private void NextSubtitle()
    {
        subIndex++;
        if (subIndex < subtitles.Length)
        {
            var newPosition = new Vector3(0,0,0);
            var newFont = FontStyle.Normal;
            string sub = subtitles[subIndex];
            if (sub.StartsWith("S:"))
            {
                sub = sub.Substring(2);
                newPosition = signePosition;
            }
            else if (sub.StartsWith("R:"))
            {
                sub = sub.Substring(2);
                newPosition = rubenPosition;
            }
            else if (sub.StartsWith("FX:"))
            {
                sub = sub.Substring(3);
                newFont = FontStyle.Italic;
            }
            if ((subIndex + 1) % 2 == 0)
            {
                subTwo.GetComponent<Text>().text = sub;
                subTwo.GetComponent<RectTransform>().anchoredPosition = newPosition;
                subTwo.GetComponent<Text>().fontStyle = newFont;
                animator.SetTrigger("next_subtitle");
            }
            else
            {
                subOne.GetComponent<RectTransform>().anchoredPosition = newPosition;
                subOne.GetComponent<Text>().text = sub;
                subOne.GetComponent<Text>().fontStyle = newFont;
                animator.SetTrigger("next_next_subtitle");
            }
        } else
        {
            startAnimation = false;
            subIndex = 0;
            animator.SetTrigger("end_subtitles");   
        }
        
    }

    public void ResetSubtitles()
    {
        subIndex = 0;
        timer = 0f;
        startAnimation = false;
        animator.ResetTrigger("enter_subtitle");
        animator.ResetTrigger("next_subtitle");
        animator.ResetTrigger("next_next_subtitle");
        animator.ResetTrigger("end_subtitles");
        animator.ResetTrigger("end_subtitles_on_subone");
        animator.ResetTrigger("end_subtitles_on_subtwo");
        subOne.GetComponent<Text>().text = subtitles[subIndex];
    }


}
