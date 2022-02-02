using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public float attackRange = 0.5f;
    public int attackDamage = 10;

    public LayerMask enemyLayer;
    private Animator _animator;

    private Transform _attackPoint;  // testing if lukas formulation works first.

    private ExplosionSmoke explosionSmoke;
    private DestroyableObject destroyableObject;

    // testing to se if I can find attackpoint.
    [SerializeField] private Transform _aimTransform;

    //SFX
    [SerializeField] AudioClip[] meleeSounds;

    //Attack-rate
    public float attackRate = 3f;       // For how often enemy attacks
    float nextAttackTime = 0f;

    public GameObject slashEffect;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _aimTransform = transform.Find("Aim");
        _attackPoint = _aimTransform.Find("AttackPoint");
    }
    void Update()
    {
        Attack();

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    // Play an attack animation
        //    _animator.SetTrigger("Attack");

        //    // Detect enemies in range of attack
        //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackRange, enemyLayer);

        //    // Damage all enemies hit
        //    foreach (Collider2D enemy in hitEnemies)
        //    {
        //        print("Hit: " + enemy.name);

        //        enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        //    }
        //}


    }
private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            // Play an attack animation
            _animator.SetTrigger("Attack");

            GameObject effect = Instantiate(slashEffect, _attackPoint.position, _attackPoint.rotation);    // create slash effect
            Destroy(effect, 10.5f);   // destroy slash after set time

            //SFX
            AudioClip clip = meleeSounds[UnityEngine.Random.Range(0, meleeSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);

            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackRange);

            // Damage all enemies hit
            foreach (Collider2D enemy in hitEnemies)
            {
                print("Hit: " + enemy.name);
                if (enemy.tag == "Enemy")
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                }
                else if (enemy.tag == "Boss")
                {
                    enemy.GetComponent<BossHealth>().TakeDamage(attackDamage);

                }
                else if (enemy.gameObject.tag == "Destroyable")
                {
                    if (enemy.gameObject.GetComponent<DestroyableObject>() != null)
                    {
                        destroyableObject = enemy.gameObject.GetComponent<DestroyableObject>();
                        destroyableObject.MeleeHit(true);
                    }
                    else if (enemy.gameObject.GetComponent<ExplosionSmoke>() != null)
                    {
                        explosionSmoke = enemy.gameObject.GetComponent<ExplosionSmoke>();
                        explosionSmoke.MeleeHit(true);
                    }                    
                }
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}

