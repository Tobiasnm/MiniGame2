using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    public Object enemyPrefab;
    public bool trigger = false;
    public Transform spawnPoint;
    public Transform[] patrolPoints;

    private EnemyHandler enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(trigger)
        {
            trigger = false;
            var newEnemy = (GameObject)Instantiate(enemyPrefab, spawnPoint.position,Quaternion.identity);
            enemy = newEnemy.GetComponent<EnemyHandler>();
            enemy.points = patrolPoints;
        }
	}
}
