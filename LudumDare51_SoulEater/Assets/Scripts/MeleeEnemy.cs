using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        animator.GetComponent<EnemySword>().Attack();
    }
}
