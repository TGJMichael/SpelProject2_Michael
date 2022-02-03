using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
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

    // Movement
    public float moveSpeed = 7.5f;
    private float dashSpeed;
    public float dashLenght = 75f;

    //Vectors and Quaternions
    Vector2 move;   
    Vector2 lastMove; //private by default

    //aiming controller
    public Vector3 aimDirection;
    private Transform aimTransform;

    //recoil when shooting test
    public float recoilForce;

    // Root
    public float rootedMovement = 1f;
    private Coroutine rootCooldown;
    private bool _rooted = false;    // changed from _canRoot, and changed all true to false and vice versa. Just to make it more readable.
    // Root effect
    public Animator _effectAnimator;
    // Rooted Dash
    public float rootedDashLength = 30f;
    public bool rootBreakDash = false;

    //SFX
    [SerializeField] AudioClip[] dashSounds;
    [SerializeField] AudioClip[] hurtSounds;

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
        //_animator = GetComponentInChildren<Animator>();
        _staminaSystem = GetComponent<StaminaSystem>();

        Animator[] allAnimatorsInChildren = GetComponentsInChildren<Animator>();    // declare and initialize "allAnimatorsInChildren" to = all animators in the children gameObjects.
        foreach (Animator animator in allAnimatorsInChildren)                       // go through the array
        {
            if (animator.tag == "OnPlayerEffect")                                   // the tag "OnPlayerEffect" is found,,
            {
                _effectAnimator = animator;                                          // and assign "_effect" to the animator that is found
            }
            if (animator.tag == "PlayerSprite")
            {
                _animator = animator;
            }
        }

        _animator.SetFloat("LastVertical", -1);
        state = State.Normal;

        //root test
        //rootedDashLength = dashLenght * 0.5f;   // if we want the lenght of dash when rooted to be half the original dash.
        
    }

    private void Update()
    {
        Movement();
        Animator();
        HandleAiming();

        if (Input.GetButtonDown("Fire2"))
        {
        _rigidbody.AddForce(-aimDirection.normalized * recoilForce, ForceMode2D.Impulse);
        }

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

    //Start of aiming block
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
    // End of aiming block

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

                GameObject Player = gameObject;
                StaminaSystem staminaSystem = Player.GetComponent<StaminaSystem>();                

                if (Input.GetKeyDown(KeyCode.Space) && (staminaSystem.currentStamina) > 9)
                {
                    //SFX
                    AudioClip clip = dashSounds[UnityEngine.Random.Range(0, dashSounds.Length)];
                    GetComponent<AudioSource>().PlayOneShot(clip);

                    // original dash.
                    //dashSpeed = dashLenght; 
                    //state = State.Dash;
                    //_animator.SetTrigger("Dash");
                    //GetComponent<StaminaSystem>().SpendStamina(10);

                    //new dash with rooted bool.
                    if (_rooted == false)   // if not rooted
                    {
                        dashSpeed = dashLenght;
                        state = State.Dash;
                        _animator.SetTrigger("Dash");
                        GetComponent<StaminaSystem>().SpendStamina(10);
                    }
                    else                    // if rooted
                    {
                        if (rootBreakDash)      // if dash can break root
                        {
                            dashSpeed = rootedDashLength;
                            state = State.Dash;
                            _animator.SetTrigger("Dash");
                            GetComponent<StaminaSystem>().SpendStamina(20);
                            print("root ended normaly with dash");
                            moveSpeed = 7.5f;
                            _effectAnimator.SetBool("IsRooted", false);
                            _rooted = false;
                        }
                        else
                        {
                        dashSpeed = rootedDashLength;
                        state = State.Dash;
                        _animator.SetTrigger("Dash");
                        GetComponent<StaminaSystem>().SpendStamina(10);
                        }
                    }
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

    private void Animator()
    {       
        _animator.SetFloat("Horizontal", move.x);
        _animator.SetFloat("Vertical", move.y);

        // Aim float parameters
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

    public void Root(float rootDuration)
    {
        _animator.SetTrigger("Hurt");
        if (_rooted == false)
        {
            _rooted = true;
            //moveSpeed = 0;   // movespeed is now connected to rootedMovement
            moveSpeed = rootedMovement;
            _effectAnimator.SetBool("IsRooted", true);

            //StopCoroutine(rootCooldown);  //Commented out just for the invurnability, if this line is active the cooldown will reset each hit(I believe)
            rootCooldown = StartCoroutine(IsRooted(rootDuration));
        }
    }
    private IEnumerator IsRooted(float rootDuration)
    {
        //moveSpeed = 0;
        print("is rooted");
        yield return new WaitForSeconds(rootDuration);
        print("root ended normaly");
        moveSpeed = 7.5f;
        _effectAnimator.SetBool("IsRooted", false);
        yield return new WaitForSeconds(1f);  // invurnability duration
        _rooted = false;
    }    

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        _animator.SetTrigger("Hurt");
        //SFX
        AudioClip clip = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Length)];
        GetComponent<AudioSource>().PlayOneShot(clip);

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