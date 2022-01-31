using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currenthealth;
    public HealthBar healthBar;

    private Animator _animator;

    //NewNextScene deathCount; // does not work
    //public DeathCounterScript m_deathCounter;
    //public int killPoint = 1;
    public NextStage killCount;

    // Player regen ammo when this unity is killed.         // works but will activate on ranged kill aswell. dont want that
    //public RangedAttack ammoRegenOnKill;

    // Item drop.
    //public Transform ItemDrop;   // dont think this is needed.
    public GameObject HearthPrefab;
    public GameObject staminaPrefab;
    //public List<GameObject> Items;
    //public int[] table =
    //{
    //    70,  // No drop
    //    15, // Health
    //    15 // Stamina
    //};
    //public int total;
    public int randomNumber;
    private void Start()
    {
        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();

        //deathCount = gameObject.GetComponent<NewNextScene>(); // does not work
        //m_deathCounter = GameObject.FindObjectOfType(typeof(DeathCounterScript)) as DeathCounterScript;
        killCount = GameObject.FindObjectOfType(typeof(NextStage)) as NextStage;


        // Player regen ammo when this unity is killed          // works but will activate on ranged kill aswell. dont want that
        //ammoRegenOnKill = GameObject.FindObjectOfType(typeof(RangedAttack)) as RangedAttack;

        // random number generator.
        randomNumber = Random.Range(0, 100);

        //foreach (var item in table)   // moved to start() since random int is generated just once.
        //{
        //    total += item;
        //}
        //randomNumber = Random.Range(0, total);

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

    void Die()
    {
        //Debug.Log("Enemy died");
        _animator.SetBool("IsDead", true);
        //print("Killed: " + gameObject.name);
        Destroy(gameObject, 5f);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        //GetComponent<EnemyRangedAttack>().enabled = false;
        //deathCount.CheckPlayerCanGoNextLevel();    // does not work. 
        //_deathCounter.GetComponent<DeathCounterScript>().KillCount(killPoint);
        //m_deathCounter.KillCount();

        // Item drop


        //Instantiate(staminaPrefab, transform.position, Quaternion.identity);
        //Instantiate(HearthPrefab, transform.position, Quaternion.identity);
        killCount.KillCount();
        ////{
        //    if (randomNumber < 30)
        //    {

        //    }
            if (randomNumber < 30 && randomNumber > 15)
            {
                Vector3 position = transform.position;
                Instantiate(staminaPrefab, position, Quaternion.identity);
            }
            if (randomNumber < 15)
            {
                Vector3 position = transform.position;
                Instantiate(HearthPrefab, position, Quaternion.identity);
            }
        //}


        // Player regen ammo when this unity is killed  // works but will activate on ranged kill aswell. dont want that
        //ammoRegenOnKill.AmmoRegenOnKill();

    }
}
