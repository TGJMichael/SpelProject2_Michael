using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 30;
    public int currenthealth;
    public HealthBar healthBar;
    public Transform ItemDrop;
    public GameObject HeartPrefab;
    public GameObject staminaPrefab;
    public List<GameObject> Item;
    public int[] table = {
        70, // No Drop
        15, // Health
        15  // Stamina
    };

    public int total;
    public int randomNumber;

    private Animator _animator;

    //NewNextScene deathCount; // does not work
    //public DeathCounterScript m_deathCounter;
    //public int killPoint = 1;
    public NextStage killCount;

    public void Start()
    {

        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();

        //deathCount = gameObject.GetComponent<NewNextScene>(); // does not work
        //m_deathCounter = GameObject.FindObjectOfType(typeof(DeathCounterScript)) as DeathCounterScript;
        killCount = GameObject.FindObjectOfType(typeof(NextStage)) as NextStage;
        
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        healthBar.SetHealth(currenthealth);

        _animator.SetTrigger("Hurt");

        if (currenthealth <= 0)
        {

            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died");
        _animator.SetBool("IsDead", true);
        print("Killed: " + gameObject.name);
        Destroy(gameObject, 10f);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;

        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);
        {
            if (randomNumber > 30)
            {
                Vector3 position = transform.position;
                Instantiate(staminaPrefab, position, Quaternion.identity);
            }
            if (randomNumber < 30 && randomNumber > 15)
            {
                
            }
            if (randomNumber < 15)
            {
                Vector3 position = transform.position;
                Instantiate(HeartPrefab, position, Quaternion.identity);
            }
        }
        //GetComponent<EnemyRangedAttack>().enabled = false;
        //deathCount.CheckPlayerCanGoNextLevel();    // does not work. 
        //_deathCounter.GetComponent<DeathCounterScript>().KillCount(killPoint);
        //m_deathCounter.KillCount();
        killCount.KillCount();


        // Add exp to player here? have to wait and see what the other one has written for code, I think he was making something.

    }
}
