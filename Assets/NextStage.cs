using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private int NextScene;
    GameObject SpawnerScript;
    public GameObject EnemySpawner;
    public bool isDead = false;

    private void Start()
    {
        NextScene = SceneManager.GetActiveScene().buildIndex + 1;

        //GetComponent<SpawnerScript>().isDead;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NextScene);
    }
}
