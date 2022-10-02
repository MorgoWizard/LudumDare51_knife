using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider))]
public class Sword : MonoBehaviour
{
    private Animator animator;
    private BoxCollider triggerCollider;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float cooldown = 1.5f;
    private bool canAttack = true;

    [SerializeField] private EventController eventController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 1f / cooldown;

        triggerCollider = GetComponent<BoxCollider>();
        triggerCollider.enabled = false;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!canAttack || !Input.GetMouseButtonDown(0))
            return;

        triggerCollider.enabled = true;
        animator.SetTrigger("Attack");
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
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            if(eventController.fireSword) other.GetComponent<StateChecker>().SetOnFire();
        }
    }

    public void DisableCollider() => triggerCollider.enabled = false;
}
