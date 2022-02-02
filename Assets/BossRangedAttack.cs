using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    private Transform _target;
    public float range;   // remove range requirement or change so boss reaches whole room(not into bridge).
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;

    // Individual shots
    public float attackRate = 1f;  // mby not needed, need to break down animations and use animationblocker instead probably.
    float nextAttackTime = 0f;
    // Salvo
    public float salvoRate = 10;
    private float _nextSalvoAttackTime = 0f;
    public int TotalSalvoShots = 5;
    [SerializeField]
    private int _salvoShotsTaken = 0;

    public GameObject projectilePrefab;      
    // testing to see if I can have the prefabs in array
    //public GameObject[] ArrayOfprojectiles;  //not not this way, better to change sprite in the projectile i think.
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
    

    // Stop moving when shooting.. mby should move bool logics here? instead of pulling it from "SimpleBossController"
    public bool Moving;

    private SimpleBossController _move;

    // shooting timer
    private float _coolDownTimer;
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
            _salvoShotsTaken = 5;
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

        // shoot on timer.
        if (_coolDownTimer < 2)
        {
            _coolDownTimer += 1 * Time.deltaTime;
            //_move.Move();
            print(_coolDownTimer);
            if(_coolDownTimer >=2 && _coolDownTimer < 10)
            {
                rootEffect = true;
                EnemyShoot();
                _coolDownTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //EnemyShoot();
            SalvoLeadUp();
            _salvoShotsTaken = 0;
            _coolDownTimer = 10;
        }
    }

    public void SalvoLeadUp()
    {
        //if (_salvoShotsTaken < TotalSalvoShots)
        //if (Time.time >= _nextSalvoAttackTime)
        //{
        //    _move.StopMove();
        //    _animator.SetTrigger("LeadUp");
        //}
        rootEffect = false;
        _move.StopMove();
        _animator.SetTrigger("LeadUp");

    }
    public void SalvoEvent(bool Salvo)
    {
        if (Salvo && _salvoShotsTaken < TotalSalvoShots)
        {
            _animator.SetBool("Salvo", true);
            //_salvoShotsTaken++;
        }
        else if (Salvo && _salvoShotsTaken >= TotalSalvoShots)
        {
            _animator.SetBool("Salvo", false);
            _salvoShotsTaken = 0;
        }

    }
    public void SalvoShoot(bool SalvoShoot)
    {
        if (SalvoShoot)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            _salvoShotsTaken++;

            if (_salvoShotsTaken >= TotalSalvoShots)
            {
                _animator.SetBool("Salvo", false);
                //_move.Move();
                //_coolDownTimer = 0;
            }
        }
    }
    public void SalvoReset(bool SalvoReset)
    {
        if (SalvoReset)
        {
            _move.Move();
            _coolDownTimer = 0;
        }
    }
    public void EnemyShoot()
    {
        if (Time.time >= nextAttackTime) 
        {
            _move.StopMove();
            _animator.SetTrigger("Spit");
            //print("Shooting towards " + _target.name);

            //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            //Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            //rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextAttackTime = Time.time + 1f / attackRate;

        }
        //else if (Time.time <= nextAttackTime)
        //{

        //    _move.Move();
        //}
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

            _move.Move();

        }
        else
        {
            print("did not shoot?");
        }
    }
}
