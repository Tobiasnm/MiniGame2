using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class HandleRain : MonoBehaviour
{
    
    public RainScript theRain;
    [Header("Select if repeating rain only")]
    public float beginRain = 15;
    public bool rainRepeating = false;

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

    public void StartRainInSeconds(float seconds)
    {
        Invoke("StartRain",seconds);
        //AkSoundEngine.PostEvent("Thunderstrike", gameObject);
        AkSoundEngine.SetRTPCValue("RainIntensity", 1, rain, 1000);
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
        if (rainRepeating)
            InvokeRepeating("StartRain", beginRain, repeatEverySec + rainDuration);

        rain = GameObject.FindGameObjectWithTag("RainAudio");
    }

    // Update is called once per frame
    void Update()
    {

        if (isRaining)
        {
            isRaining = false;
            Invoke("stopRain", rainDuration);
        }

    }
}
