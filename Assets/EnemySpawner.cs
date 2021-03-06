﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numEnemies;

    public override void OnStartServer()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8.0f, 8.0f), 0.2f, Random.Range(-8.0f, 8.0f));
            Quaternion rot = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, pos, rot);
            NetworkServer.Spawn(enemy);
        }

    }
	
}
