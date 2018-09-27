using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    private Animator animator;

    private string scenename;

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

    public void FadeComplete()
    {
        SceneManager.LoadScene(scenename);
    }

    public void FadeMenuIn()
    {
        animator.SetTrigger("menu_fade_in");
    }
}
