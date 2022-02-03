using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public EnemyRangedAttack enemyRangedAttack;
    public EnemyController enemyController;
    // Boss
    public SimpleBossController simpleBossController;
    public BossRangedAttack bossRangedAttack;

    //public bool AttackEvent;

    void Start()
    {
        enemyRangedAttack = GetComponentInParent<EnemyRangedAttack>();
        enemyController = GetComponentInParent<EnemyController>();
        //Boss
        simpleBossController = GetComponentInParent<SimpleBossController>();
        bossRangedAttack = GetComponentInParent<BossRangedAttack>();
    }
    public void RangedAttackTrigger()
    {
        enemyRangedAttack.RangedAttackEvent(true);
    }
    public void MeleeAttackTrigger()
    {
        enemyController.MeleeAttackEvent(true);
    }

    // Boss
    public void BossMeleeAttackTrigger()
    {
        simpleBossController.MeleeAttackEvent(true);
    }
    public void BossRangedAttackTrigger()
    {
        bossRangedAttack.RangedAttackEvent(true);
    }
    public void SalvoAttackTrigger()
    {
        bossRangedAttack.SalvoEvent(true);
    }
    public void SalvoShootTrigger()
    {
        bossRangedAttack.SalvoShoot(true);
    }
    public void SalvoResetTrigger()
    {
        bossRangedAttack.SalvoReset(true);
    }
}
