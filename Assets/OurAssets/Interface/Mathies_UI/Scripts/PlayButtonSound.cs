using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour {

    public void OnClick()
    {
        AkSoundEngine.PostEvent("Play_Button1",gameObject);
    }

    public void StopMusic()
    {
        AkSoundEngine.PostEvent("Stop_MenuTrack1", GameObject.Find("MenuTheme"));
    }
}
