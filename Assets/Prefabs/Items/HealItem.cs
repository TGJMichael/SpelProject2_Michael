using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    //public GameObject Player;
    //public int maxHealth = 30;
    //public int currentHealth;
    public int HealAmount = 10;
    //public GameObject HeartPrefab;
    //public GameObject Heart;
    //public HealthBar healthBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().HealDamage(HealAmount);
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))

    //    {
    //        if (Heart && currentHealth + HealAmount < maxHealth)
    //        {
    //            Destroy(Heart);
    //            currentHealth += HealAmount;
    //            healthBar.SetHealth(currentHealth);

    //        }

    //    }
    //    if (Heart && currentHealth + HealAmount > maxHealth)
    //    {
    //        Destroy(Heart);
    //        currentHealth = maxHealth;
    //        healthBar.SetHealth(currentHealth);


    //    }
    //}
}
