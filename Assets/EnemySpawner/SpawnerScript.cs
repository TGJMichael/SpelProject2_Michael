using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public GameObject[] SpawnPoints;
    private float timer;
    private int spawnIndex;
    public bool isDead = false;

    // killcheck addon from "NewNextScene"

    void Start()
    {
        Instantiate(EnemyPrefab, SpawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(EnemyPrefab, SpawnPoints[1].transform.position, Quaternion.identity);
        timer = Time.time + 3.0f;
    }


    void Update()
    {
        if (timer < Time.time && !isDead)
        {
            Instantiate(EnemyPrefab, SpawnPoints[spawnIndex % 2].transform.position, Quaternion.identity);
            timer = Time.time + 3.0f;
            spawnIndex++;

        }

    }
}

