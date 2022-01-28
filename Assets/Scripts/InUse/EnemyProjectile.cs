using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private int normalDamage;
    private int rootDamage;
    private float rootDuration = 5f;

    public float TimeToLive = 3f;

    // for contact animation
    public GameObject hitEffect;

    public bool rootEffect;

    private void Start()
    {

        normalDamage = FindObjectOfType<EnemyRangedAttack>().normalDamage;
        rootDamage = FindObjectOfType<EnemyRangedAttack>().rootDamage;
        rootEffect = FindObjectOfType<EnemyRangedAttack>().rootEffect;
        rootDuration = FindObjectOfType<EnemyRangedAttack>().rootDuration;
        StartCoroutine(destroyProjectile());
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
            }
            else
            {
                targetHit.GetComponent<HealthSystem>().TakeDamage(normalDamage);
            }
            //ExplodeAndDestroy();
            //StartCoroutine(CharacterController.instance.Root(rootDuration));

            //targetHit.GetComponent<CharacterController>().Root(rootDuration);

            //lägga till en collider som är trigger "zone of effect" - kan användda denna vid träff av projectil med projectil

            //starta movement-speed slowing coroutine.

        }
        else
        {
            print("Can't hurt: " + targetHit.name);
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
