using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject projectilePrefab;

    public float projectileForce = 20f;     // "speed" of projectile.. kinda

    private Animator _animator;

    // "Rate of fire"
    public float attackRate = 5f;
    float nextAttackTime = 0f;

    // ammo count 
    public int currentAmmo;
    public int numOfProjectiles;

    public Image[] projectiles;
    public Sprite projectile;
    public Sprite emptyProjectile;

    // ammoregen timer
    private float _coolDownTimer;

    //SFX
    [SerializeField] AudioClip[] rangeSounds;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        currentAmmo = numOfProjectiles;
    }
    private void Update()
    {
        // ammo count sprites for ui
        if (currentAmmo > numOfProjectiles)   // this makes sure that currentAmmo count dont go over total ammo alowed.
        {
            currentAmmo = numOfProjectiles;
        }
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (i < currentAmmo)
            {
                projectiles[i].sprite = projectile;
            }
            else
            {
                projectiles[i].sprite = emptyProjectile;
            }
            if (i < numOfProjectiles)
            {
                projectiles[i].enabled = true;
            }
            else
            {
                projectiles[i].enabled = false;
            }
        }
        // ammo regen
        if (currentAmmo < numOfProjectiles && _coolDownTimer < 1.5)
        {
            _coolDownTimer += 1 * Time.deltaTime;
            //print(_coolDownTimer);
            if (_coolDownTimer >= 1.5)
            {
                currentAmmo++;
                _coolDownTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && (currentAmmo > 0))    // ammo-count instead of bar.
        {
            Shoot();
        }
    }

    public void AmmoRegenOnKill()
    {
        currentAmmo = numOfProjectiles;
    }

    void Shoot()
    {
        currentAmmo -= 1;

        _animator.SetTrigger("RangedAttack");

        //SFX
        AudioClip clip = rangeSounds[UnityEngine.Random.Range(0, rangeSounds.Length)];
        GetComponent<AudioSource>().PlayOneShot(clip);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);

        nextAttackTime = Time.time + 1f / attackRate;
    }
}
