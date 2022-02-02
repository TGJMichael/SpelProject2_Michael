using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private int normalDamage;
    private int rootDamage;
    private float rootDuration = 5f;

    public float TimeToLive = 3f;

    // for contact animation
    public GameObject hitEffect;
    public GameObject webEffect;
    public GameObject explosionEffect;

    public bool rootEffect;

    // color-change 
    SpriteRenderer m_SpriteRenderer;

    // knockback and hurt animation for when rootEffeckt = false;
    public float knockbackPower = 0.2f;
    public float knockbackDuration = 10f;

   public BossRangedAttack Salvo;

    private void Start()
    {
        normalDamage = FindObjectOfType<BossRangedAttack>().normalDamage;
        rootDamage = FindObjectOfType<BossRangedAttack>().rootDamage;
        rootEffect = FindObjectOfType<BossRangedAttack>().rootEffect;
        rootDuration = FindObjectOfType<BossRangedAttack>().rootDuration;
        StartCoroutine(destroyProjectile());
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Salvo = GameObject.FindObjectOfType(typeof(BossRangedAttack)) as BossRangedAttack;

        // Size and color-change.
        if (rootEffect)
        {
            m_SpriteRenderer.color = Color.white;
            transform.localScale = new Vector3(2, 2, 2); // I wanna try to see if i can make 1 public float to controll all three vectors,
            hitEffect = webEffect;
        }
        else
        {
            m_SpriteRenderer.color = Color.red;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            hitEffect = explosionEffect;
        }
        //size change works.
        //if (rootEffect)
        //{
        //    transform.localScale = new Vector3(2, 2, 2);
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject targetHit = collision.gameObject;
        if (targetHit.tag == "Player")
        {
            print("Hit: " + targetHit.name);
            if (rootEffect)
            {
                targetHit.GetComponent<CharacterController>().Root(rootDuration);
                targetHit.GetComponent<HealthSystem>().TakeDamage(rootDamage);
                Salvo.SalvoLeadUp();
            }
            else
            {
                targetHit.GetComponent<HealthSystem>().TakeDamage(normalDamage);

                StartCoroutine(CharacterController.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            }
            //ExplodeAndDestroy();
            //StartCoroutine(CharacterController.instance.Root(rootDuration));

            //targetHit.GetComponent<CharacterController>().Root(rootDuration);

            //lägga till en collider som är trigger "zone of effect" - kan användda denna vid träff av projectil med projectil

            //starta movement-speed slowing coroutine.

        }
        else
        {
            //print("Can't hurt: " + targetHit.name);
        }
        ExplodeAndDestroy();
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);    // create explosion animation on contact position
        //Destroy(effect, 5f);   // destroy animation after set time (5f) 
        //Destroy(gameObject);   // destroy projectile

    }
    private void ExplodeAndDestroy()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);    // create explosion animation on contact position
        Destroy(effect, 5f);   // destroy animation after set time (5f) 
        Destroy(gameObject);   // destroy projectile
    }
    private IEnumerator destroyProjectile()
    {
        Destroy(gameObject, TimeToLive);
        yield return new WaitForSeconds(TimeToLive);
    }
}
