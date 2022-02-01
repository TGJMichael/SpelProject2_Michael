using System.Collections;
using UnityEngine;

public class ExplosionSmoke : MonoBehaviour
{
    private Animator _animator;
    private ExplosionSmoke explosionSmoke;
    private DestroyableObject destroyableObject;

    [SerializeField] int attackDamage = 10;
    [SerializeField] float attackRange = 10f;

    public float knockbackPower = 0.5f;
    public float knockbackDuration = 10f;

    // state variables
    [SerializeField] int timesHit;

    // for contact animation
    public GameObject hitEffect;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //HandleHit();
        if (collision.gameObject.tag == "Projectile")
        {
            HandleHit();
        }
    }

    public void MeleeHit(bool AttackHit)
    {
        if (AttackHit)
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = 0;
        if (timesHit == maxHits)
        {
            return;
            //DestroyBlock();
        }
        else
        {
            _animator.SetTrigger("Explode");
        }
    }

    private void ExplodeAndDestroy()
    {

        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);    // create explosion animation on contact position
        Destroy(effect, 5f);   // destroy animation after set time (5f) 

        // Damage
        Collider2D[] smokeHit = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D hit in smokeHit)
        {
            print("Smoke Hit: " + hit.name);

            if (hit.tag == "Player")
            {
                StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
                hit.GetComponent<HealthSystem>().TakeDamage(attackDamage);
            }
            else if (hit.tag == "Enemy")
            {
                hit.GetComponent<EnemyHealth>().TakeDamage(attackDamage);                
            }
            else if (hit.tag == "Boss")
            {
                hit.GetComponent<BossHealth>().TakeDamage(attackDamage);

            }
            else if (hit.gameObject.tag == "Destroyable")
            {
                if (hit.gameObject.GetComponent<DestroyableObject>() != null)
                {
                    destroyableObject = hit.gameObject.GetComponent<DestroyableObject>();
                    destroyableObject.MeleeHit(true);
                }
                else if (hit.gameObject.GetComponent<ExplosionSmoke>() != null)
                {
                    explosionSmoke = hit.gameObject.GetComponent<ExplosionSmoke>();
                    explosionSmoke.MeleeHit(true);
                }
            }
        }
    }
}
