using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currenthealth;
    public HealthBar healthBar;

    private Animator _animator;

    private void Start()
    {
        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        healthBar.SetHealth(currenthealth);

        _animator.SetTrigger("Hurt");

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        _animator.SetBool("IsDead", true);
        print("Killed: " + gameObject.name);
        Destroy(gameObject, 5f);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
             

        // Add exp to player here? have to wait and see what the other one has written for code, I think he was making something.
        
    }
}
