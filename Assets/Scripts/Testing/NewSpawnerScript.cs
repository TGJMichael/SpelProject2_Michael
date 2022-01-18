using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnerScript : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public GameObject[] SpawnPoints;
    private float timer;
    private int spawnIndex;
    public bool isDead = false;

    // amount of enemies needed to kill block - test
    public int NeedEnemyDiedToGoNextScene;
    public int CurrentEnemyDied;

    //bool for spawn timer
    public bool spawnTimer;

    //spawn points
    //private int _spawnPosition = 0;
    void Start()
    {
        
        foreach (var item in SpawnPoints)
        {
            Instantiate(EnemyPrefab, SpawnPoints[1].transform.position, Quaternion.identity);
        }
        //Instantiate(EnemyPrefab, SpawnPoints[0].transform.position, Quaternion.identity);
        //Instantiate(EnemyPrefab, SpawnPoints[1].transform.position, Quaternion.identity);
        timer = Time.time + 3.0f;
    }

    void Update()
    {
        // test spawning in connection with index amount.
        //for ()
        {

        }
        if (spawnTimer)
        {

        if (timer < Time.time && !isDead)
        {
            Instantiate(EnemyPrefab, SpawnPoints[spawnIndex % 2].transform.position, Quaternion.identity);
            timer = Time.time + 3.0f;
            spawnIndex++;

        }
        }

    }

}

