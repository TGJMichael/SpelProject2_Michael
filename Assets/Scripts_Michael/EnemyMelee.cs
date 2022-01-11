using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float attackRange = 2f;

    public int attackDamage = 3;
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            print("Hit: " + target.name);
            StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));

            target.GetComponent<HealthSystem>().TakeDamage(attackDamage);

                nextAttackTime = Time.time + 1f / attackRate;
        }
        }
    }
}
