using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackEvent : MonoBehaviour
{
    public EnemyRangedAttack enemyRangedAttack;
    public bool AttackEvent;

    // Start is called before the first frame update
    void Start()
    {
        enemyRangedAttack = GetComponentInParent<EnemyRangedAttack>();
    }

    public void RangedAttackTrigger()
    {
        enemyRangedAttack.RangedAttackEvent(true);
    }
}
