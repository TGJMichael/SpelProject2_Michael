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

    // regeneration
    //public float healtRegSpeed = 3;
    //public int healthRegCooldownTime = 1;
    //[SerializeField]
    //private bool healthRegOn;
    //private Coroutine healthCooldown;

    //float fix
    private float decimalhealth;

    // Item drop                // seing as I don't think this is needed i will comment it out. Will leave if here for now,
    //public int HealAmount = 10;
    //public GameObject HeartPrefab;
    //public GameObject Heart;


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

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))   // this should not work at all since the collision we are looking for are "Item" not "Player" thats what this code is on.
    //                                                     //GetComponent<HealthSystem>();  // we already are inside HealthSystem.
    //    {
    //        if (HeartPrefab && currentHealth + HealAmount < maxHealth)
    //        {
    //            Destroy(Heart);
    //            currentHealth += HealAmount;
    //            healthBar.SetHealth(currentHealth);
    //        }
    //    }
    //    if (HeartPrefab && currentHealth + HealAmount > maxHealth)
    //    {
    //        Destroy(Heart);
    //        currentHealth = maxHealth;
    //        healthBar.SetHealth(currentHealth);
    //    }
    //}
    // Item drop heal
    public void HealDamage(int heal)
    {
        currentHealth += heal;

        healthBar.SetHealth(currentHealth);  // is it really neccesary for this to be set everytime health changes?

        if (currentHealth >= maxHealth)  // try later to se if "maxHealth - heal" works
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
