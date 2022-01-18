using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public EnemyRangedAttack enemyRangedAttack;
    public EnemyController enemyController;

    //public bool AttackEvent;

    void Start()
    {
        enemyRangedAttack = GetComponentInParent<EnemyRangedAttack>();
        enemyController = GetComponentInParent<EnemyController>();
    }
    public void RangedAttackTrigger()
    {
        enemyRangedAttack.RangedAttackEvent(true);
    }
    public void MeleeAttackTrigger()
    {
        enemyController.MeleeAttackEvent(true);
    }
}
