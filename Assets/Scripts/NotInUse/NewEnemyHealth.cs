using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyHealth : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public HealthbarBehaviour Healthbar;

    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }
    public void TakeHit(float damage)
    {
        Hitpoints -= damage;

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
