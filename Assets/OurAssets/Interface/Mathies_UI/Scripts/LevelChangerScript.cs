using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    private Animator animator;

    public string scenename;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToLevel (string levelname)
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;
        scenename = levelname;
        animator.SetTrigger("menu_fade_out");
    }

    public void SkipCutscene()
    {
        AkSoundEngine.StopAll();
        animator.SetTrigger("end_subtitles");
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene(scenename);
    }

}
