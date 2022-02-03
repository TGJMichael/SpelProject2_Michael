using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBossController : MonoBehaviour
{
    // Movement
    public float moveSpeed = 4f;
    private bool moveRight;

    private Animator _animator;

    private Transform _target;

    // Melee attack
    public float attackRange = 3f;      // distance to when enemy start to attack
    public int attackDamage = 2;
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;
    // Melee attack rate
    public float attackRate = 3f;
    private float _nextAttackTime;

    public bool Moving = true;

    // Shooting
    public BossRangedAttack Shoot;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        moveRight = true;   // is there a difference in assigning value here instead of when defining "moveRight"?
        _target = FindObjectOfType<CharacterController>().transform;

        // Shooting
        Shoot = GameObject.FindObjectOfType(typeof(BossRangedAttack)) as BossRangedAttack;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))  // toggle to stop and start Boss from moving.
        {
            Moving = !Moving;
        }
        if (Moving == false)
        {
            _animator.SetBool("Moving", false);
            //Shoot.EnemyShoot();

        }

        Movement();


        if (Vector3.Distance(transform.position, _target.transform.position) < attackRange)
        {
            MeleeAttack();
        }
    }

    // With this "BossRangedAttack" can controll when to stop moving.
    public void Move()
    {
        Moving = true;
    }
    public void StopMove()
    {
        Moving = false;
    }
    public void Movement()
    {
        if (Moving == true)
        {

            if (transform.position.x > 7f)  // 7f is how far to the right the boss moves before
            {
                moveRight = false;          // bool sets to false wich will result in boss start moving to the left
                _animator.SetFloat("Horizontal", -1);
            }
            else if (transform.position.x < -7f)
            {
                moveRight = true;
                _animator.SetFloat("Horizontal", 1);
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);  // moving right on x position, left position stays constant
                _animator.SetBool("Moving", true);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);  // moving left "-   -   -   -   -   -   -   -   -   -   -"
                _animator.SetBool("Moving", true);
            }
        }
    }

    private void MeleeAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("Attack");

            _nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void MeleeAttackEvent(bool AttackHit)
    {
        if (AttackHit)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < attackRange)
            {
                StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));

                _target.GetComponent<HealthSystem>().TakeDamage(attackDamage);

                //SFX
                //AudioClip clip = bite         // add later..
            }
            //else          // more audion stuff, add later... 
            //{

            //}
        }
    }
}
