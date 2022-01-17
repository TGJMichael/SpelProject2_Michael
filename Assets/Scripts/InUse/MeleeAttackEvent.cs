using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;  // from "EnemyGFX"
using System;

public class MeleeAttackEvent : MonoBehaviour
{
    public EnemyController enemyController;
    public bool AttackEvent;

    public void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }
    public void MeleeAttackTrigger()
    {
        enemyController.MeleeAttackEvent(true);

        //enemyController.MeleeAttackEvent(false);
    }
}
