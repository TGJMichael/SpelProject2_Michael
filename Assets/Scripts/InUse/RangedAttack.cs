using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject projectilePrefab;

    public float projectileForce = 20f;     // "speed" of projectile.. kinda

    private Animator _animator;

    // "Rate of fire"
    public float attackRate = 5f;
    float nextAttackTime = 0f;

    //// recharge and spit(ammo)
    public float maxSpit = 5;       // each spit costs (1)
    public float currentSpit;
    public float spitRegSpeed = 3;
    public float spitRegCooldownTime = 1.5f;

    public SpitBar spitbar;

    [SerializeField]
    private bool spitRegOn;
    private Coroutine spitCooldown;

    // brackeys video
    //public int maxAmmo = 10;
    //private int currentAmmo;
    //public float reloadTime = 1f;

    //private bool _isReloading = false;

    private void Start()
    {
        spitbar = FindObjectOfType<SpitBar>();
        _animator = GetComponentInChildren<Animator>();

        // recharge and "ammo"
        currentSpit = maxSpit;
        spitbar.SetMaxSpit(maxSpit);

        spitCooldown = StartCoroutine(spitRegCooldown());

        // brackeys video
        //currentAmmo = maxAmmo;
    }
    private void Update()
    {
        // recharge and spit(ammo)
        if (spitRegOn && currentSpit < maxSpit)
        {
            currentSpit += spitRegSpeed * Time.deltaTime;
            spitbar.SetSpit(currentSpit);
        }
        //brackeys video
        //if (_isReloading)    // if _isreloading is true, dont do anything below in Update.
        //    return;
        //if (currentAmmo <= 0)
        //{
        //    StartCoroutine(Reload());
        //    return;
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextAttackTime)  // just by attack-rate
        if (Input.GetKeyDown(KeyCode.Mouse1) && (currentSpit > 1))    // by same system as stamina.
        {
            Shoot();
        }
    }

    // recharge and spit(ammo)
    public void SpendSpit (int spent)
    {
        currentSpit -= spent;
        spitbar.SetSpit(currentSpit);

        StopCoroutine(spitCooldown);
        spitCooldown = StartCoroutine(spitRegCooldown());
    }

    private IEnumerator spitRegCooldown()
    {
        spitRegOn = false;

        yield return new WaitForSeconds(spitRegCooldownTime);

        spitRegOn = true;
    }
    //brackeys video
    //IEnumerator Reload()
    //{
    //    _isReloading = true;
    //    Debug.Log("Reloading...");

    //    yield return new WaitForSeconds(reloadTime);

    //    currentAmmo = maxAmmo;
    //    _isReloading = false;
    //}

    void Shoot()
    {
        //recharge and spit(ammo)
        SpendSpit(1);
        // brackeys video
        //currentAmmo--;

        _animator.SetTrigger("RangedAttack");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);

        nextAttackTime = Time.time + 1f / attackRate;
        
    }
}
