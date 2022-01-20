using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private int NextScene;
    //public GameObject deathCounter;  // since i am combining scripts, dont need this.

    // Kill-counter
    public int killsNeeded = 3;
    public int currentKills = 0;

    public bool objectiveComplete = false;

    // Stuff to deactivate when objectiveComplete
    public Transform Door;

    //last scene fix
    public bool lastScene = false;
    private int _firstScene;
    private void Start()
    {
        Door = this.gameObject.transform.GetChild(0);

        NextScene = SceneManager.GetActiveScene().buildIndex + 1;
        //NextScene = SceneManager.GetActiveScene().buildIndex - 1;   // this is just for testing since i dont have more that 2 scenes and the one i am working on is the second on the index.
        _firstScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void KillCount()
    {
        currentKills++;
        if (currentKills == killsNeeded)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        objectiveComplete = true;
        Door.gameObject.SetActive(false);   // 

    }

    public void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.tag != "Player")  // will ensure only the player can activate NextScene
            return;
        //if (EnemySpawner.GetComponent<NewSpawnerScript>().isDead == true)   // changed this to a new bool since i have moved were the enemy kills are counted.
        //if (deathCounter.GetComponent<DeathCounterScript>().enemiesStillAlive == false) trying out to combine deathcounter with this code so i am changeing this
        if (objectiveComplete == true)
        {
            SceneManager.LoadScene(NextScene);
            print("trying to enter new scene");

            if (lastScene)
            {
                SceneManager.LoadScene("StartMenu");
            }
        }

    }
}

