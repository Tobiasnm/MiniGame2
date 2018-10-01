using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [Range(0,1)]
    public float triggerEventAtPercentage = 0.0f;

    private Queue<GameObject> obstacles = new Queue<GameObject>();

    [SerializeField]
    int tapsToBreak = 10;

    [SerializeField]
    int tapsDone = 0;

    [SerializeField]
    string tapSound;

    // Use this for initialization
    void Start()
    {
        MeshRenderer[] renderedChildren = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in renderedChildren)
        {
            obstacles.Enqueue(child.gameObject);
            Debug.Log(child.name);
        }
        
    }

    public void RegisterTap()
    {
        tapsDone += 1;
        AkSoundEngine.PostEvent(tapSound, gameObject);
        this.GetComponentInChildren<ParticleSystem>().Emit(Mathf.CeilToInt(200/tapsToBreak));
        Debug.Log("Tap");
    }

    private void DestroyObstacle()
    {
        //Do before destruction
        if (obstacles.Count > 0)
        {
            Destroy(obstacles.Dequeue());
            if (obstacles.Count < triggerEventAtPercentage)
                TriggerEvent();
        }
        if (obstacles.Count <= 0)
            Destroy(this.gameObject);
    }

    private void TriggerEvent()
    {
        Debug.Log("Event triggered!");
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacles.Count>0 && tapsDone*obstacles.Count >= tapsToBreak)
        {
            DestroyObstacle();
        }
    }


}

