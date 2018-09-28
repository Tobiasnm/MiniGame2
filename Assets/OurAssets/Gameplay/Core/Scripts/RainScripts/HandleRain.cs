using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class HandleRain : MonoBehaviour
{
    
    public RainScript theRain;
    [Header("Select if repeating rain only")]
    public bool startAndRepeatRain = false;
    public float beginRain = 15;
    

    [Header("Select anyway")]
    public float repeatEverySec = 10;
    public float rainDuration = 10;
    private bool isRaining = false;
    private GameObject rain;

    public void StartRain()
    {
        isRaining = true;
        theRain.RainIntensity = 1;
        AkSoundEngine.SetRTPCValue("RainIntensity", 4, rain, 3000);
        Debug.Log("Rain begun!");
    }

    private void DoThunder()
    {
        AkSoundEngine.PostEvent("Play_Thunderclap", gameObject);
    }

    public void StartRainInSeconds(float startRainInSeconds, float doThunderInSeconds)
    {
        Invoke("StartRain", startRainInSeconds);
        Invoke("DoThunder", doThunderInSeconds);
    }

    public void StopRain()
    {
        isRaining = false;
        theRain.RainIntensity = 0;
        AkSoundEngine.SetRTPCValue("RainIntensity", 0, rain, 4000);
        Debug.Log("Rain stopped!");
    }

    public void Awake()
    {
        StopRain();
    }

    // Use this for initialization
    void Start()
    {
        StopRain();
        if (startAndRepeatRain)
            InvokeRepeating("StartRain", beginRain, repeatEverySec + rainDuration);

        rain = GameObject.FindGameObjectWithTag("RainAudio");
    }

    // Update is called once per frame
    void Update()
    {

        if (isRaining)
        {
            isRaining = false;
            Invoke("StopRain", rainDuration);
        }

    }
}
