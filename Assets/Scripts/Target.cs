using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;

    public bool pushable = false;
    public bool destroyable = true;

    public float knockbackPower = 10f;
    public float knockbackDuration = 10f;

    public Rigidbody2D _rigidbody;

    Vector2 direction;

    void Start()
    {
        currentHealth = maxHealth;
        GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(gameObject.transform.position);
    }

    public void TakeDamage(int damage)
    {
        if (destroyable)
        {
            currentHealth -= damage;
        }        

        if (pushable)
        {
            GameObject player = GameObject.Find("Player");
            Transform playerTransform = player.transform;
            direction = (gameObject.transform.position - player.transform.position).normalized;
            _rigidbody.AddForce(direction * knockbackPower * 1000);
            //StartCoroutine(Knockback());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("Destroyed: " + gameObject.name);
        Destroy(gameObject);
    }

    public IEnumerator Knockback()
    {
        print("Knockback?");

        /*test.x = 1f;
        test.y = 1f;
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;*/

        float timer = 0;
        
        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            print("Knockback!");
            _rigidbody.AddForce(direction * knockbackPower);
        }        
        yield return 0;
    }
}