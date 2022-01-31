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

    public NextStage killCount;

    // Item drop
    public GameObject HearthPrefab;
    public GameObject staminaPrefab;
    // Random generator for item drop
    public int randomNumber;
    private void Start()
    {
        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();
;
        killCount = GameObject.FindObjectOfType(typeof(NextStage)) as NextStage;


        randomNumber = Random.Range(0, 100);

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

        // Item drop
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

        killCount.KillCount();
    }
}
