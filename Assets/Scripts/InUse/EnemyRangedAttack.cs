using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRangedAttack : MonoBehaviour
{
    private Transform _target;
    public float range;
    public float knockbackPower = 10f;
    public float knockbackDuration = 10f;
    
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    private Animator _animator;

    // Damage for the "EnemyProjectile" prefab to pull from
    [Header("Projectile")]
    public int normalDamage = 3;
    public int rootDamage = 0;
    public float rootDuration = 5f;
    // bool that changes the ranged attack from normal to root.
    public bool rootEffect = true;

    // Start and stop Chase when in range
    private EnemyController _enemyController;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _target = FindObjectOfType<CharacterController>().transform;

        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, _target.transform.position) < range)
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;  

            EnemyShoot();
            _animator.SetBool("Ranged", true);
        }
        else
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            _animator.SetBool("Ranged", false);
        }
    }

    private void EnemyShoot()
    {
        if (Time.time >= nextAttackTime)
        {
            _animator.SetTrigger("Attack");
            print("Hit: " + _target.name);

            //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            //Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            //rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void RangedAttackEvent(bool AttackHit)
    {
        if(AttackHit)
        {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

        }
        else
        {
            print("did not shoot?");
        }
    }
}
