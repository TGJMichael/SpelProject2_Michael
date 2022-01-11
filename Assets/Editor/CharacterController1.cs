using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
public class OldCharacterController : MonoBehaviour
{
    public static OldCharacterController instance;
    private enum State
    {
        Normal,
        Dash,
    }

    private State state;

    //Grabbing things automatically
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private StaminaSystem _staminaSystem;

    //Grabbing things manually
    public GameObject meleeAttackVisual;
    public GameObject projectileObject;
    public Transform interactor;
    public Transform interactPoint;

    //Layer stuff
    public LayerMask enemyLayer;

    //Bools
    bool dashButtonDown;

    //Ints and floats
    public float moveSpeed = 5f;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = 0.1f;
    private float dashSpeed;
    public float dashInput = 75f;


    //Vectors and Quaternions
    Vector2 attackPoint;
    Vector2 move;
    Vector2 lastMove;
    Vector2 aimAngle;
    Quaternion projectileRotation;

    //Coroutines
    Coroutine shootingCoroutine;

    //new aiming controller
    //public Camera cam;
    //Vector2 mousePos;
    public Transform _firePoint;

    // newest addition to aiming controller
    public Vector3 shootPosition;
    public Vector3 aimDirection;

    private Transform aimTransform;

    private void Awake()
    {
        instance = this;

        //new aim
        aimTransform = transform.Find("Aim");
    }

    private void Start()
    {      
        _rigidbody = GetComponent<Rigidbody2D>();        
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();        
        _animator = GetComponentInChildren<Animator>();
        _staminaSystem = GetComponent<StaminaSystem>();

        _animator.SetFloat("LastVertical", -1);
        state = State.Normal;

        
    }

    private void Update()
    {
        Movement();
        AimAngle();
        Animator();
        Interactor();
        Attack();
        //Shoot();



        // new aiming controller
        HandleAiming();

        //NewAimAngle(); //commenting out my newaimangle since i am working on another new way to aim.

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 lookDir = mousePos - _rigidbody.position; // this part calculates direction from players rigidbody to mouseposition 
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;    // finds x axis towards directional vector //removed - 90f from this line since char gets ofsett. and physically rotatet firepoint z to correct position.
        // part of above but for testing purposes this part will rotate character, will remove this part later if rest works as intended, will probable need to change spawn point of projectile since it hangs on this rotation.
        //_rigidbody.rotation = angle;        
    }


    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                Vector2 topSpeed = move.normalized;
                _rigidbody.MovePosition(_rigidbody.position + topSpeed * moveSpeed * Time.fixedDeltaTime);

                break;
            case State.Dash:
                _rigidbody.velocity = lastMove * dashSpeed;
                break;
        }

    }

    //Start of new aiming block
    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        aimDirection = (mousePosition - transform.position).normalized;
        float angle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    // End of new aiming block

    private void Movement()
    {
        switch (state) 
        {
            case State.Normal:
                move.x = Input.GetAxisRaw("Horizontal");
                move.y = Input.GetAxisRaw("Vertical");

                if (move.x != 0 || move.y != 0)
                {
                    lastMove = move.normalized;                    
                }

                if (lastMove.x == 0 && lastMove.y == 0)
                {
                    lastMove.y = -1;
                }

                //GameObject Player = GameObject.Find(gameObject.name);   // instead of  "Player" you can use (gameObject.name) unless you only want the script to work on ("What is inside brackets")
                GameObject Player = gameObject;         // detta skippar find.This scipts find and goes directly to the gameObject the component in question is connected to. better than line above.
                StaminaSystem staminaSystem = Player.GetComponent<StaminaSystem>();                

                if (Input.GetKeyDown(KeyCode.Space) && (staminaSystem.currentStamina) > 9)
                {
                    dashSpeed = dashInput;
                    state = State.Dash;
                    _animator.SetTrigger("Dash");
                    GetComponent<StaminaSystem>().SpendStamina(10);
                }
                break;
            case State.Dash:
                
                float dashSpeedDropMultiplier = 10f;
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                

                float rollSpeedMinimum = 10f;
                if (dashSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                }
                break;
        }
        
    }
    
    private void AimAngle()
    {
        aimAngle = move.normalized;

        if (aimAngle.x == 0 && aimAngle.y == 0)
        {
            aimAngle.x = _animator.GetFloat("LastHorizontal");
            aimAngle.y = _animator.GetFloat("LastVertical");
            if (aimAngle.x == 0 && aimAngle.y == 0)
            {
                aimAngle.y = -1;
            }
        }
    }

    private void Animator()
    {       
        _animator.SetFloat("Horizontal", move.x);
        _animator.SetFloat("Vertical", move.y);

        // new aim float parameters
        _animator.SetFloat("HorizontalAim", aimDirection.x);
        _animator.SetFloat("VerticalAim", aimDirection.y);

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

    private void Interactor()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
        }

        attackPoint = (interactPoint.position + transform.position) / 2;
        /*if (aimAngle.y < 0)
        {
            attackPoint = interactPoint.position;
        }*/
    }

    // commenting out my newaimangle since i am working on another new way to aim
    //private void NewAimAngle()
    //{ 
    //        // what should show the aiming angle? animate the head or whole body towards aimangle.

    //    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 lookDir = mousePos - _rigidbody.position; // this part calculates direction from players rigidbody to mouseposition 
    //    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;    // finds x axis towards directional vector //removed - 90f from this line since char gets ofsett. and physically rotatet firepoint z to correct position.
    //    //part of below but for testing purposes this part will rotate character, will remove this part later if rest works as intended, will probable need to change spawn point of projectile since it hangs on this rotation.
    //   _rigidbody.rotation = angle;

        
    //}
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Play an attack animation
            _animator.SetTrigger("Attack");

            /*StopCoroutine(MeleeVisual());
            StartCoroutine(MeleeVisual());*/

            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemyLayer);

            // Damage all enemies hit
            foreach (Collider2D enemy in hitEnemies)
            {
                print("Hit: " + enemy.name);

                //enemy.GetComponent<Target>().TakeDamage(attackDamage);  // changed this to line under since i am changing the enemies codes. 
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }        
    }

    IEnumerator MeleeVisual()
    {
        meleeAttackVisual.GetComponent<SpriteRenderer>().enabled = true;        
        yield return new WaitForSeconds(0.2f);

        meleeAttackVisual.GetComponent<SpriteRenderer>().enabled = false;        
        yield return new WaitForSeconds(0);
    }

    void OnDrawGizmosSelected()
    {
        if(interactor == null)
        {        
            return;
        }
        Gizmos.DrawWireSphere(attackPoint, attackRange);
    }
    // commenting out this shooting sections for now since I am revorking how players aim and shoot
    //private void Shoot()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        shootingCoroutine = StartCoroutine(FireContinously());
    //    }
    //    if (Input.GetKeyUp(KeyCode.Mouse1))
    //    {
    //        StopCoroutine(shootingCoroutine);
    //    }
    //}

    IEnumerator FireContinously()
    {
        while (true)
        {          
            ProjectileRotation();
            _animator.SetTrigger("Attack");

            GameObject projectile = Instantiate(projectileObject, attackPoint, projectileRotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = aimAngle * projectileSpeed;
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void ProjectileRotation()
    {        
        if (aimAngle.x > 0)
        {
            projectileRotation = Quaternion.Euler(0, 0, -90);
        }
        if (aimAngle.x < 0)
        {
            projectileRotation = Quaternion.Euler(0, 0, 90);
        }
        if (aimAngle.y > 0)
        {
            projectileRotation = Quaternion.Euler(0, 0, 0);
        }
        if (aimAngle.y < 0)
        {
            projectileRotation = Quaternion.Euler(0, 0, 180);
        }

    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        _animator.SetTrigger("Hurt");
        float timer = 0;
        while (knockbackDuration > timer)
        {            
            timer += Time.deltaTime;            
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            _rigidbody.AddForce(-direction * knockbackPower);
        }
        yield return 0;
    }


}
#endif