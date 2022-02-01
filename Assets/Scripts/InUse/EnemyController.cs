using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyController : MonoBehaviour
{
    private AIPath _aiPath;

    private Animator _animator;
    private Transform _target;

    public float aggroRange = 5;     //how close player need to be for ai to chase
    public Rigidbody2D rigibody;

    // For the melee attack
    public float attackRange = 3f;      // distance to when enemy start to attack
    public int attackDamage = 2;
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;
    public float attackRate = 3f;       // For how often enemy attacks
    float nextAttackTime = 0f;

    Vector2 move;

    // CalculateDirection
    private int _rightLeftValue;
    private int _upDownValue;

    //SFX
    [SerializeField] AudioClip[] biteSounds;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _target = FindObjectOfType<CharacterController>().transform;
        GetComponent<AIDestinationSetter>().target = _target;   // if actively changing in game this 
        _aiPath = GetComponent<AIPath>();

    }

    void Update()
    {

        CalculateDirection();
        Movement();
        Animator();

        if (Vector3.Distance(transform.position, _target.transform.position) > aggroRange)
        {
            GetComponent<AIPath>().isStopped = true;
        }
        else
        {
            GetComponent<AIPath>().isStopped = false;
        }

        if (Vector3.Distance(transform.position, _target.transform.position) < attackRange)
        {
            print("attacking");
            MeleeAttack(); 
        }
    }

    private void Animator()
    {
        //_animator.SetFloat("Horizontal", move.x);     // made more precise calculations for the animator, new output below these two
        //_animator.SetFloat("Vertical", move.y);       // same as above

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

    }
    public void CalculateDirection()
    {
        //float _absoluteMoveX = Mathf.Abs(move.x);     //(1)  // these are just for testing purposes trying out what works, all works but is it nessecary? prob not.
        //float _absoluteMoveY = Math.Abs(move.y);      //(1)  
        //if (_absoluteMoveX >_absoluteMoveY)           //(1)
        //float _maxXY = Mathf.Max(_absoluteMoveX, _absoluteMoveY); // (1,2)
        //if (_maxXY == _absoluteMoveX )  // (1,2)
        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))  // if the horizontal(left/right) absolute value is the biggest

        {
            _upDownValue = 0;
            //print("facing right or left");
            if (move.x > 0)                         // if facing right                     
            {
                //print("facing right");
                _rightLeftValue = 1;
            }
            else                                    // if facing left
            {
                //print("facing left");
                _rightLeftValue = -1;
            }
        }
        else                                        // if vertical(up/down) absolute value is the biggest
        {
            _rightLeftValue = 0;
            //print("facing up or down");
            if (move.y > 0)                         // if facing up        
            {
                //print("facing up");
                _upDownValue = 1;
            }
            else                                    // if facing down
            {
                //print("facing down");
                _upDownValue = -1;
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
                StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));

                _target.GetComponent<HealthSystem>().TakeDamage(attackDamage);

                //SFX
                AudioClip clip = biteSounds[UnityEngine.Random.Range(0, biteSounds.Length)];
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            else
            {
                print("Missed");

                //SFX
                AudioClip clip = biteSounds[UnityEngine.Random.Range(0, biteSounds.Length)];
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
        }        
    }
}
