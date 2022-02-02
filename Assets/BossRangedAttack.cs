using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    private Transform _target;
    public float range;   // remove range requirement or change so boss reaches whole room(not into bridge).
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    private Animator _animator;

    //SFX
    [SerializeField] AudioClip[] spitSounds;

    // Damage for the "EnemyProjectile" prefab to pull from
    [Header("Projectile")]
    public int normalDamage = 3;
    public int rootDamage = 0;
    public float rootDuration = 5f;

    // bool that changes the ranged attack from normal to root.
    public bool rootEffect = true;   // should make two methods, one than instantiate rooting projectiles and one that instantiate normal projectiles.


    // Stop moving when shooting.. mby should move bool logics here? instead of pulling it from "SimpleBossController"   // not needed if shooting is controlled by S.B.Controller.
    public bool Moving;

    private SimpleBossController _move;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _target = FindObjectOfType<CharacterController>().transform;

        //_enemyController = GetComponent<EnemyController>();   // not needed.
        Moving = GetComponent<SimpleBossController>().Moving;

        _move = GetComponent<SimpleBossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //GetComponent<SimpleBossController>().Move();
            _move.Move();
        }

        // Normal shooting activation,, now the shooting is done from "SimpleBossController"
        //if (Vector3.Distance(transform.position, _target.transform.position) < range)
        //{
        //    EnemyShoot();
        //    //_animator.SetBool("Ranged", true);   // dont think "Ranged" stuff is needed..
        //}
        //else
        //{
        //    //_animator.SetBool("Ranged", false);  
        //}

        if (Input.GetKeyDown(KeyCode.L))
        {
            EnemyShoot();
        }
    }

    public void EnemyShoot()
    {
        if (Time.time >= nextAttackTime)        // change logic to shoot 3 times then w8 for long time. mby i should have a long timer and break up the animation, loop same exakt frames when spitting while shooting intead of one per animation.
        {
            _move.StopMove();
            _animator.SetTrigger("Spit");
            //print("Shooting towards " + _target.name);

            //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            //Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            //rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextAttackTime = Time.time + 1f / attackRate;

        }
        else if (Time.time <= nextAttackTime)
        {

            _move.Move();
        }
    }
    public void RangedAttackEvent(bool AttackHit)
    {
        if (AttackHit)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            //SFX
            AudioClip clip = spitSounds[UnityEngine.Random.Range(0, spitSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);          // something is missing for this to work.

        }
        else
        {
            print("did not shoot?");
        }
    }
}
