using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject projectilePrefab;

    public float projectileForce = 20f;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //if(Input.GetButtonDown("Fire2"))   // "Fire2" is for both right mouse button and left alt
        //{
        //    Shoot();
        //}        
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        _animator.SetTrigger("RangedAttack");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);

        //add recoil to player
        
    }
}
