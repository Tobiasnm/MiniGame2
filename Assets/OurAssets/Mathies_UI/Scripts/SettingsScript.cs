using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public AudioMixer musicMixer;
    public AudioMixer audioMixer;

    private bool danish = true;

    public void SetVibration(bool vibration)
    {
        int vibInt = vibration ? 1 : 0;
        PlayerPrefs.SetInt("vibration", vibInt);

        if (vibration)
        {
            Handheld.Vibrate();
        }
    }

    public void SetMusic(float volume)
    {
        musicMixer.SetFloat("musicVolume", volume);
    }

    public void SetAmbience(float volume)
    {
        audioMixer.SetFloat("ambienceVolume", volume);
    }

    public void ChangeLangauge()
    {
        danish = danish ? false : true;
        if (danish)
        {
            Debug.Log("Dansk");
            PlayerPrefs.SetString("language", "danish");
        } else
        {
            Debug.Log("English");
            PlayerPrefs.SetString("language", "english");
        }
    }
}
