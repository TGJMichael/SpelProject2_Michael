using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int attackDamage = 10;

    public float TimeToLive = 3f;

    // for contact animation
    public GameObject hitEffect;

    private void Start()
    {

        StartCoroutine(destroyProjectile());
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject targetHit = collision.gameObject;
        if (targetHit.tag == "Enemy")
        {
            //print("Hit: " + targetHit.name);

            targetHit.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            


        }   
        if (targetHit.tag == "Boss")
        {
            targetHit.GetComponent<BossHealth>().TakeDamage(attackDamage);
        }
        else
        {
            //print("Can't hurt: " + targetHit.name);
        }
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);    // create explosion animation on contact position
        Destroy(effect, 5f);   // destroy animation after set time (5f) 
        Destroy(gameObject);   // destroy projectile
    }

    private IEnumerator destroyProjectile()
    {
        Destroy(gameObject, TimeToLive);
        yield return new WaitForSeconds(TimeToLive);
    }
}
