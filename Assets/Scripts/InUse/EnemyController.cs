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

    //testing new animator parameters
    [SerializeField]
    private int _rightLeftValue;
    [SerializeField]
    private int _upDownValue;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _target = FindObjectOfType<CharacterController>().transform;
        GetComponent<AIDestinationSetter>().target = _target;   // if actively changing in game this 
        _aiPath = GetComponent<AIPath>();



    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    CalculateDirection();
        //}
        CalculateDirection();
        Movement();
        Animator();
        //Aggro();

        if (Vector3.Distance(transform.position, _target.transform.position) > aggroRange)
        {
            GetComponent<AIPath>().isStopped = true;
        }
        else
        {
            GetComponent<AIPath>().isStopped = false;
        }
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
        //_animator.SetFloat("Horizontal", move.x);
        //_animator.SetFloat("Vertical", move.y);

        _animator.SetFloat("Horizontal", _rightLeftValue);
        _animator.SetFloat("Vertical", _upDownValue);


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
            //_animator.SetFloat("LastHorizontal", move.x);
            //_animator.SetFloat("LastVertical", move.y);
            _animator.SetFloat("LastHorizontal", _rightLeftValue);
            _animator.SetFloat("LastVertical", _upDownValue);
        }
    }

    private void Movement()
    {
        move.x = _aiPath.desiredVelocity.x;
        move.y = _aiPath.desiredVelocity.y;

        // moved to own method.
        //// compare absolute value between x and y to see if the player is above/below or left/right of this enemy.
        //if (Mathf.Abs(move.x) > Mathf.Abs(move.y))  // if the horizontal(left/right) absolute value is the biggest
        //{
        //    print("facing right or left");
        //    if (move.x > 0)                         // if facing right                     
        //    {
        //        print("facing right");
        //    }
        //    else                                    // if facing left
        //    {
        //        print("facing left");
        //    }
        //}
        //else                                        // if vertical(up/down) absolute value is the biggest
        //{
        //    print("facing up or down");
        //    if (move.y > 0)                         // if facing up        
        //    {
        //        print("facing up");
        //    }
        //    else                                    // if facing down
        //    {
        //        print("facing down");
        //    }
        //}

    }
    public void CalculateDirection()
    {
        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))  // if the horizontal(left/right) absolute value is the biggest
        {
            _upDownValue = 0;
            print("facing right or left");
            if (move.x > 0)                         // if facing right                     
            {
                print("facing right");
                _rightLeftValue = 1;
            }
            else                                    // if facing left
            {
                print("facing left");
                _rightLeftValue = -1;
            }
        }
        else                                        // if vertical(up/down) absolute value is the biggest
        {
            _rightLeftValue = 0;
            print("facing up or down");
            if (move.y > 0)                         // if facing up        
            {
                print("facing up");
                _upDownValue = 1;
            }
            else                                    // if facing down
            {
                _upDownValue = -1;
                print("facing down");
            }
        }
    }

    public void MeleeAttack ()
    { 
        if (Time.time >= nextAttackTime)
        {
            _animator.SetTrigger("Attack");
            
            
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void MeleeAttackEvent(bool AttackHit)
    {
        if (AttackHit)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < attackRange)
            {
                print("Hit: " + _target.name);
                StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knocbackPower, this.transform));

                _target.GetComponent<HealthSystem>().TakeDamage(attackDamage);
            }
            else
            {
                print("Missed");
            }
        }        
    }
}
