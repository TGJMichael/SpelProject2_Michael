using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private Transform _target;
    public float range;
    public int attackDamagage = 3;
    public float knockbackPower = 10f;
    public float knockbackDuration = 10f;
    
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileForce = 20f;
    void Start()
    {

        _target = FindObjectOfType<CharacterController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < range)
        {
            print("Target Within range");
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            EnemyShoot();
        }
        else
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void EnemyShoot()
    {
        if (Time.time >= nextAttackTime)
        {
            //_animator.SetTrigger("RangedAttack");
            print("Hit: " + _target.name);

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);

            //StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));

            //_target.GetComponent<HealthSystem>().TakeDamage(attackDamagage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}
