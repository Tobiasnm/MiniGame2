using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class HandleRain : MonoBehaviour
{
    //public GameObject rain;
    public RainScript theRain;
    [Header("Select if repeating rain only")]
    public float beginRain = 15;
    public bool rainRepeating = false;

    [Header("Select anyway")]
    public float repeatEverySec = 10;
    public float rainDuration = 10;
    private bool isRaining = false;

    public void startRain()
    {
        isRaining = true;
        theRain.RainIntensity = 1;
        //AkSoundEngine.SetRTPCValue("Rain_Intensity", 5, rain, 3000);
        Debug.Log("Rain begun!");
    }

    public void startRainInSeconds(float seconds)
    {
        Invoke("startRain",seconds);
        //AkSoundEngine.PostEvent("Thunderstrike", gameObject);
        //AkSoundEngine.SetRTPCValue("Rain_Intensity", 1, rain, 1000);
    }

    public void stopRain()
    {
        isRaining = false;
        theRain.RainIntensity = 0;
        //AkSoundEngine.SetRTPCValue("Rain_Intensity", 0, rain, 4000);
        Debug.Log("Rain stopped!");
    }

    public void Awake()
    {
        stopRain();
    }

    // Use this for initialization
    void Start()
    {
        stopRain();
        if (rainRepeating)
            InvokeRepeating("startRain", beginRain, repeatEverySec + rainDuration);
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
