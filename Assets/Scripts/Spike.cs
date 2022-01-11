using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int attackDamage = 10;
    public float knockbackPower = 10f;
    public float knockbackDuration = 10f;

    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject targetHit = collision.gameObject;
        if (targetHit.tag == "Player")
        {
            print("Hit: " + targetHit.name);

            StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));

            targetHit.GetComponent<HealthSystem>().TakeDamage(attackDamage);
        }
    }
}
