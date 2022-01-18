using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounterScript : MonoBehaviour
{
    public int killsNeeded = 3;
    public int currentKills = 0;

    public bool enemiesStillAlive;
    void Start()
    {
        //currentKills = killsLeft;
    }

    //public void KillCount(int killPoint)
    public void KillCount()
    {
        currentKills ++;
        
        if (currentKills == killsNeeded)
        {
            ActivateDoor();
        }
    }

    private void ActivateDoor()
    {
        enemiesStillAlive = false;
        //GetComponent<Collider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        //GetComponentInChildren<NextStage>().enabled = false;
    }
}
