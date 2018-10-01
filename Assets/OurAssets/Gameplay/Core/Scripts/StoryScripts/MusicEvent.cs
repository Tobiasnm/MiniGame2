using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicEvent{

    public string audioClipName;
    public float soundLevel;
    [Range(0, 1)]
    public float triggerEventAtPercentage = 0.0f;

}
