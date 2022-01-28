using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currenthealth;
    public HealthBar healthBar;

    private Animator _animator;

    //NewNextScene deathCount; // does not work
    //public DeathCounterScript m_deathCounter;
    //public int killPoint = 1;
    public NextStage killCount;

    // Player regen ammo when this unity is killed.         // works but will activate on ranged kill aswell. dont want that
    //public RangedAttack ammoRegenOnKill;
    private void Start()
    {
        currenthealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        _animator = GetComponentInChildren<Animator>();

        //deathCount = gameObject.GetComponent<NewNextScene>(); // does not work
        //m_deathCounter = GameObject.FindObjectOfType(typeof(DeathCounterScript)) as DeathCounterScript;
        killCount = GameObject.FindObjectOfType(typeof(NextStage)) as NextStage;


        // Player regen ammo when this unity is killed          // works but will activate on ranged kill aswell. dont want that
        //ammoRegenOnKill = GameObject.FindObjectOfType(typeof(RangedAttack)) as RangedAttack;
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
        //Debug.Log("Enemy died");
        _animator.SetBool("IsDead", true);
        //print("Killed: " + gameObject.name);
        Destroy(gameObject, 5f);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        //GetComponent<EnemyRangedAttack>().enabled = false;
        //deathCount.CheckPlayerCanGoNextLevel();    // does not work. 
        //_deathCounter.GetComponent<DeathCounterScript>().KillCount(killPoint);
        //m_deathCounter.KillCount();
        killCount.KillCount();


        // Player regen ammo when this unity is killed  // works but will activate on ranged kill aswell. dont want that
        //ammoRegenOnKill.AmmoRegenOnKill();

    }
}
