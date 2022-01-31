using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currenthealth;
    public HealthBar healthBar;

    private Animator _animator;

    public CameraShake _cameraShake;

    private bool _halfLifeShake = true;
    void Start()
    {
        _cameraShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        healthBar.SetHealth(currenthealth);

        _animator.SetTrigger("Hurt");

        if (currenthealth <= maxHealth * 0.5 && _halfLifeShake)
        {
            _cameraShake.Shake(1);
            _halfLifeShake = false;
            // insert animator trigger for scream or something here, and mby spawn some enemies, increase dmg, increase movement speed e.t.c.
        }

        if (currenthealth <= 0)
        {

            Die();
        }
    }

    void Die()
    {
        Debug.Log("You Win");
        _animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SimpleBossController>().enabled = false;
        //GetComponentInChildren<Canvas>().enabled = false;   // cant do this because the canvas is not a child.
        healthBar.gameObject.SetActive(false);
        //CameraShake.Shake(0.25f, 4f);
        _cameraShake.Shake(4);
    }
    
    void Update()
    {
        
    }
}
