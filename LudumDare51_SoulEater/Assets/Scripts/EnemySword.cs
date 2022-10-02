using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private BoxCollider triggerCollider;
    
    [SerializeField] private float damage = 1f;
    [SerializeField] private float cooldown = 1.5f;
    private bool canAttack = true;

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
        triggerCollider.enabled = false;
    }

    public void Attack()
    {
        if (!canAttack)
            return;

        triggerCollider.enabled = true;
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void DisableCollider() => triggerCollider.enabled = false;
}
