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
    public float moveSpeed = 5f;
    private float dashSpeed;    // will probably make a separate "Dash" ability.
    public float dashInput = 75f;


    //Vectors and Quaternions
    Vector2 move;   
    Vector2 lastMove; 

    //aiming controller
    public Vector3 aimDirection;
    private Transform aimTransform;

    // test fix for aimingdirection. (this is a bad sollution but I´ll try this just to test the concept)
    [SerializeField] private int _aimObjDirection;

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
        Animator();
        HandleAiming();

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
        aimTransform.eulerAngles = new Vector3(0, 0, angle);   // mby in () i can change so firepoint move when character moves?
        //print(angle);
        //print(aimDirection);
        //print(GetDirectionFromAngle(angle));   // jonte

        ////testing for the aiming direction objects
        //if (angle > -135 && angle < -45) //down
        //    {
        //        _aimObjDirection = 1;
        //        aimTransform.localPosition = new Vector3(0, -0.6f, 0);
        //        //aimTransform.localRotation = Quaternion.AngleAxis()
        //    }
        //    else if (angle > 135 || angle < -135) // left
        //    {
        //        _aimObjDirection = 2;
        //        aimTransform.localPosition = new Vector3(-0.6f, 0, 0);
        //    }
        //    else if (angle < 135 && angle > 45) // up
        //    {
        //        _aimObjDirection = 4;
        //        aimTransform.localPosition = new Vector3(0, 0.6f, 0);
        //    }
        //    else   // right
        //    {
        //        _aimObjDirection = 3;
        //        aimTransform.localPosition = new Vector3(0.6f, 0, 0);
        //    }
    }

    //private Vector2 GetDirectionFromAngle(float angleInDegrees)  // Jonte
    //{
    //    var angle = angleInDegrees;
    //    angle += aimTransform.eulerAngles.z;

    //    var AngleInRadians = angle * Mathf.Deg2Rad;
    //    var x = Mathf.Cos(AngleInRadians);
    //    var y = Mathf.Sin(AngleInRadians);
    //    return new Vector2(x, y);
    //}

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

                GameObject Player = gameObject;
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