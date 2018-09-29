using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{

    [SerializeField]
    int tapsToBreak = 10;

    [SerializeField]
    int tapsRemaining;

    [SerializeField]
    string tapSound;

    // Use this for initialization
    void Start()
    {
        tapsRemaining = tapsToBreak;
    }

    public void RegisterTap()
    {
        tapsRemaining -= 1;
        AkSoundEngine.PostEvent(tapSound, gameObject);
        this.GetComponentInChildren<ParticleSystem>().Emit(10);
        Debug.Log("Tap");
    }

    private void DestroyObstacle()
    {
        //Do before destruction
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (tapsRemaining <= 0)
            DestroyObstacle();
    }


}

