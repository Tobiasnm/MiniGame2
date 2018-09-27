using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public AudioMixer musicMixer;
    public AudioMixer audioMixer;

    private bool danish = true;

    private Button languageButton;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (languageButton == null)
        {
            GameObject languageObject = GameObject.FindGameObjectWithTag("Language");
            if (languageObject != null) languageButton = languageObject.GetComponent<Button>();
        }
    }

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
            languageButton.GetComponentInChildren<Text>().text = "Dansk";
            PlayerPrefs.SetString("language", "danish");
        } else
        {
            languageButton.GetComponentInChildren<Text>().text = "English";
            PlayerPrefs.SetString("language", "english");
        }
    }
}
