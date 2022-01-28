using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth = 30;
    public int currentHealth;
    public int HealAmount = 10;
    public GameObject HeartPrefab;
    public GameObject Heart;
    public HealthBar healthBar;
    

    // regeneration
    //public float healtRegSpeed = 3;
    //public int healthRegCooldownTime = 1;
    //[SerializeField]
    //private bool healthRegOn;
    //private Coroutine healthCooldown;

    //float fix
    private float decimalhealth;

    void Start()
    {        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //regen
        //healthCooldown = StartCoroutine(healthRegCooldown());
    }

    //private void Update()
    //{
    //    //regen
    //    //if (healthRegOn && currentHealth < maxHealth)
    //    //{
    //    //    decimalhealth += 1 * Time.deltaTime;
    //    //    currentHealth = Mathf.RoundToInt(decimalhealth);
    //    //    //currentHealth += healtRegSpeed * Time.deltaTime;
    //    //    healthBar.SetHealth(currentHealth);
    //    //}
    //    decimalhealth += 1 * Time.deltaTime;
    //    currentHealth += Mathf.RoundToInt(decimalhealth);
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GetComponent<HealthSystem>();
        {
            if (HeartPrefab && currentHealth + HealAmount < maxHealth)
            {
                Destroy(Heart);
                currentHealth += HealAmount;
                healthBar.SetHealth(currentHealth);
                
            }

        }
        if (HeartPrefab && currentHealth + HealAmount > maxHealth)
        {
            Destroy(Heart);
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            
        }
    }
public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        //StopCoroutine(healthCooldown);
        //healthCooldown = StartCoroutine(healthRegCooldown());
    }
    
    //private IEnumerator healthRegCooldown()
    //{
    //    healthRegOn = false;

    //    yield return new WaitForSeconds(healthRegCooldownTime);

    //    healthRegOn = true;
    //}
    void Die()
    {
        print("You died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
}
