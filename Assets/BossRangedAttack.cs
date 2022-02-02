using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    private Transform _target;
    public float range;
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

    // Start and stop Chase when in range
    //private EnemyController _enemyController;     // not needed
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _target = FindObjectOfType<CharacterController>().transform;

        //_enemyController = GetComponent<EnemyController>();   // not needed.
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, _target.transform.position) < range)
        {
            //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;  // not needed i think, change to someway else to stop boss from moving... Does not work anyway.

            EnemyShoot();
            //_animator.SetBool("Ranged", true);
        }
        //else
        //{
        //    //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        //    //_animator.SetBool("Ranged", false);
        //}
    }

    private void EnemyShoot()
    {
        if (Time.time >= nextAttackTime)        // change logic to shoot 3 times then w8 for long time. mby i should have a long timer and break up the animation, loop same exakt frames when spitting while shooting intead of one per animation.
        {
            _animator.SetTrigger("Spit");
            //print("Shooting towards " + _target.name);

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    //public void RangedAttackEvent(bool AttackHit)
    //{
    //    if (AttackHit)
    //    {
    //        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    //        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    //        rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

    //        //SFX
    //        AudioClip clip = spitSounds[UnityEngine.Random.Range(0, spitSounds.Length)];
    //        GetComponent<AudioSource>().PlayOneShot(clip);

    //    }
    //    else
    //    {
    //        print("did not shoot?");
    //    }
    //}
}
