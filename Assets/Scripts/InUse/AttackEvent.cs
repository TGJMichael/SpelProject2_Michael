using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public EnemyRangedAttack enemyRangedAttack;
    public EnemyController enemyController;
    public SimpleBossController simpleBossController;

    //public bool AttackEvent;

    void Start()
    {
        enemyRangedAttack = GetComponentInParent<EnemyRangedAttack>();
        enemyController = GetComponentInParent<EnemyController>();
        simpleBossController = GetComponentInParent<SimpleBossController>();
    }
    public void RangedAttackTrigger()
    {
        enemyRangedAttack.RangedAttackEvent(true);
    }
    public void MeleeAttackTrigger()
    {
        enemyController.MeleeAttackEvent(true);
    }
    public void BossMeleeAttackTrigger()
    {
        simpleBossController.MeleeAttackEvent(true);
    }
}
