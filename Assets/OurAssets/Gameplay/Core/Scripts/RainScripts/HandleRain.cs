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
    public float darknessLevel = 0.7f;


    private bool isRaining = false;
    private GameObject rain;
    private Light theSun;
    private float sunStartingIntensity;
    private float lightTransitionDuration = 3;
    private float currTime = 0;

    public void StartRain()
    {
        isRaining = true;
        theRain.RainIntensity = 1;
        AkSoundEngine.SetRTPCValue("RainIntensity", 4, rain, 3000);
        Debug.Log("Rain begun!");
    }

    public void StopRain()
    {
        isRaining = false;
        theRain.RainIntensity = 0;
        AkSoundEngine.SetRTPCValue("RainIntensity", 0, rain, 4000);
        Debug.Log("Rain stopped!");
        InvokeRepeating("LerpToLight", 0.1f, Time.deltaTime);
    }


    private void LerpToDarkness()
    {
        Debug.Log("Started lerping to dark");

        if (currTime <= 1)
        {
            currTime += Time.deltaTime / lightTransitionDuration;

            theSun.intensity = Mathf.Lerp(theSun.intensity, darknessLevel, currTime);
        }
        if (currTime >= 1)
        {
            currTime = 0;
            Invoke("CancelLerpToDarkness", 0.5f);
        }
    }

    private void LerpToLight()
    {
        if (currTime <= 1)
        {
            currTime += Time.deltaTime/lightTransitionDuration;

            theSun.intensity = Mathf.Lerp( theSun.intensity, sunStartingIntensity, currTime);
        }

        if (currTime >= 1)
        {
            currTime = 0;
            Invoke("CancelLerpToLight",0.5f);
        }
    }

    private void CancelLerpToLight()
    {
        CancelInvoke("LerpToLight");
    }

    private void CancelLerpToDarkness()
    {
        CancelInvoke("LerpToDarkness");
    }

    private void DoThunder()
    {
        AkSoundEngine.PostEvent("Play_Thunderclap", gameObject);
    }

    public void StartRainInSeconds(float startRainInSeconds, float doThunderInSeconds)
    {
        Invoke("StartRain", startRainInSeconds);
        InvokeRepeating("LerpToDarkness", 0.1f, Time.deltaTime);
        Invoke("DoThunder", doThunderInSeconds);
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
        theSun = GameObject.FindGameObjectWithTag("TheSun").GetComponent<Light>();
        sunStartingIntensity = theSun.intensity;
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
