﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour {

    public void OnClick()
    {
        AkSoundEngine.PostEvent("Play_Button1",gameObject);
    }
}
