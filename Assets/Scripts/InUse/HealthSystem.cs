using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth = 30;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Item drop heal
    public void HealDamage(int heal)
    {
        currentHealth += heal;

        healthBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth)  // if currentHealth would exceed maxHealth when healed,ks
        {
            currentHealth = maxHealth;
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

    }
    
    void Die()
    {
        print("You died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
}
