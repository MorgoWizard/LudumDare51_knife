using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private Animator swordAnimator;

    protected override void Attack()
    {
        swordAnimator.SetTrigger("Attack");
        swordAnimator.GetComponent<EnemySword>().Attack();
    }
}
