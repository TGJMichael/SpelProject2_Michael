using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;  // from "EnemyGFX"
using System;

public class EnemyController : MonoBehaviour
{
    //private Pathfinding aidPath; // from "EnemyGFX"
    private AIPath _aiPath;
    //public AIPath canMove { get => canMove; set => canMove = value; }

    private Animator _animator;
    private Transform _target;

    public float aggroRange;     //how close player need to be for ai to chase.still havent figured out how to swicth from random checkpoints to chasing player.
    public Rigidbody2D rigibody;

    // For the melee attack
    public float attackRange = 2f;      // distance to when enemy start to attack
    public int attackDamage = 3;
    public float knocbackPower = 0.2f;
    public float knockbackDuration = 10f;
    public float attackRate = 2f;       // For how often enemy attacks
    float nextAttackTime = 0f;

    Vector2 move;
 

    private 
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _target = FindObjectOfType<CharacterController>().transform;

        _aiPath = GetComponent<AIPath>();

    }

    void Update()
    {
        Movement();
        Animator();
        //Aggro();

        // nothing of these works as intended
        //Chase();      
        //StartMoving();

        //if (Input.GetKeyUp(KeyCode.P))
        //{
        //    StopMoving();
        //}

        // expiremental aipath code for together with animator. it works as intended. will move this block to animator function tomorrow.
        //if (Vector3.Distance(transform.position, _target.transform.position) < aggroRange)
        //{
        //    print("Target Within range");
        //    gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //}



        if (Vector3.Distance(transform.position, _target.transform.position) < attackRange)
        {
            print("attacking");
            MeleeAttack(); 
        }
    }

    //private void Aggro()
    //{
    //    throw new NotImplementedException();
    //}

    private void Animator()
    {
        _animator.SetFloat("Horizontal", move.x);
        _animator.SetFloat("Vertical", move.y);

        if (move.sqrMagnitude > 1)
        {
            _animator.SetFloat("Speed", 1);
        }
        else
        {
            _animator.SetFloat("Speed", move.sqrMagnitude);
        }

        if (move.x != 0 || move.y != 0)
        {
            _animator.SetFloat("LastHorizontal", move.x);
            _animator.SetFloat("LastVertical", move.y);
        }
    }

    private void Movement()
    {
        move.x = _aiPath.desiredVelocity.x;
        move.y = _aiPath.desiredVelocity.y;

    }

    //does not work as intended. no matter what i try, the pathfinding will turn off permanently. the aipathstopper script works atleast.
    //private void Chase()
    //{
    //    if (Vector3.Distance(transform.position, _target.transform.position) > aggroRange)
    //    {
    //        _aiPath.enabled = false;
    //        //_aiPath.enabled = true;
    //        //if (_aiPath.enabled == false)
    //        //{
    //        //}
    //    }
    //    else
    //    {
    //        _aiPath.enabled = true;
    //    }
    //}

    //public void StopMoving()
    //{
    //    _aipath.enabled = !_aipath.enabled;
    //}
    //public void StartMoving()
    //{
    //    myAnim.SetBool("Ismoving", true);
    //}

    public void MeleeAttack ()
    { 
        if (Time.time >= nextAttackTime)
        {
            _animator.SetTrigger("Attack");
            print("Hit: " + _target.name);
            StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knocbackPower, this.transform));

            _target.GetComponent<HealthSystem>().TakeDamage(attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}
