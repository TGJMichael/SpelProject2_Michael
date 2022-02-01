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

    private DestroyableObject destroyableObject;

    // testing to se if I can find attackpoint.
    [SerializeField] private Transform _aimTransform;

    //SFX
    [SerializeField] AudioClip[] meleeSounds;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Play an attack animation
            _animator.SetTrigger("Attack");

            //SFX
            AudioClip clip = meleeSounds[UnityEngine.Random.Range(0, meleeSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);

            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackRange, enemyLayer);

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
                    destroyableObject = enemy.gameObject.GetComponent<DestroyableObject>();
                    destroyableObject.MeleeHit(true);
                }
            }
        }
    }
}

