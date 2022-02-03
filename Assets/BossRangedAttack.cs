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
    public int TotalSalvoShots = 5;
    [SerializeField]
    private int _salvoShotsTaken = 0;

    private float _coolDownTimer;   // rooting shot
    private float _salvoTimer;

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
    public bool rootEffect = true; 
    
    //public bool Moving;

    // Move stop functions
    private SimpleBossController _move;

    // shooting timers
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _target = FindObjectOfType<CharacterController>().transform;

        //_enemyController = GetComponent<EnemyController>();   // not needed.
        //Moving = GetComponent<SimpleBossController>().Moving;

        _move = GetComponent<SimpleBossController>();
    }

    // Update is called once per frame
    void Update()
    {
        // For testing purposes stoping movement through "SimpleBossController"
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    _move.Move();
        //}

        // shoot on timer.
        if (_coolDownTimer < 2)
        {
            _coolDownTimer += 1 * Time.deltaTime;
            
            //print(_coolDownTimer);
            if(_coolDownTimer >=2 && _coolDownTimer < 10)
            {
                rootEffect = true;
                EnemyShoot();
                _coolDownTimer = 0;
            }
        }
        if (_salvoTimer < 8)
        {
            _salvoTimer += 1 * Time.deltaTime;
            //print(_salvoTimer);

            if (_salvoTimer >= 8 && _salvoTimer < 10)
            {
                SalvoLeadUp();
                _coolDownTimer = 10;
            }
        }

        // For testing purposes activating salvo.
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    //EnemyShoot();
        //    SalvoLeadUp();
        //    _salvoShotsTaken = 0;
        //    _coolDownTimer = 10;
        //}
    }

    // Step 1 in Salvo.
    public void SalvoLeadUp()
    {
        
        _salvoShotsTaken = 0;               // Reset ammo
        _salvoTimer = 10;                   // Set Timer on 10 - buggy if not set outside of "If" parameters.
        rootEffect = false;                 // Set so that the projectile does not root and do more dmg.
        _move.StopMove();                   // Stop Boss from moving
        _animator.SetTrigger("LeadUp");     // Start first animation in the salvo, "Leadup". 

    }

    // Step 2 && 3.5 in Salvo. Start of event chain. This one if from LeadUp animation.
    public void SalvoEvent(bool Salvo)
    {
        if (Salvo && _salvoShotsTaken < TotalSalvoShots)            // If there are ammo left (Step 2 part)
        {
            _animator.SetBool("Salvo", true);                       // Set animation bool to true. So that animation after LeadUp can start, which is "Shoot" and repeat until bool is false.
        }
        else if (Salvo && _salvoShotsTaken >= TotalSalvoShots)      // If no ammo left (Step 3.5 part), might be reduntand since there is another check in Salvoshoot, but breaks _salvoTimer if removed.
        {
            _animator.SetBool("Salvo", false);                      // Stop "Shoot" and start "Reset" animation.
            _salvoShotsTaken = 0;                                   // Reset ammo, might be reduntant since I reset att the activation method. 
        }

    }

    // Step 3 in Salvo. Shooting part of event chain.
    public void SalvoShoot(bool SalvoShoot)
    {
        if (SalvoShoot)
        {
            // The instantiation of the projectiles
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            _salvoShotsTaken++;                                 // Shots taken counter, tiks up 1 each projectile instatiated.

            if (_salvoShotsTaken >= TotalSalvoShots)
            {
                _animator.SetBool("Salvo", false);
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
            _salvoTimer = 0;
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
