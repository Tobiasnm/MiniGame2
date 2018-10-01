
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour {

    private GameObject pauseGo;
    private GameObject resumeGo;

    private void Start()
    {
        pauseGo = GameObject.Find("PauseDialogue");
        resumeGo = GameObject.Find("ResumeDialogue");
    }

    public void PauseDialogue () {
        Debug.Log(pauseGo.name);
        AkSoundEngine.PostEvent("Pause_intro_dialouge", pauseGo);
    }

    public void ResumeDialogue ()
    {
        Debug.Log("resume dialog");
        AkSoundEngine.PostEvent("Resume_intro_dialouge", resumeGo);
    }
	
}
