using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 5f;

    private NavMeshAgent agent;
    protected Transform target;
    [SerializeField] private float agroRadius = 10f;
    [SerializeField] protected float attackRadius = 5f;

    private bool isFollow = false;

    private float attackCooldown = 1f;
    protected bool canAttack = true;

    [SerializeField] public Transform patrolZoneCenter;
    [SerializeField] private float randomPointRadius;
    [SerializeField] private float stopAfterPoint;
    private bool pathComplete = false;
    private bool pathCompleteCoroutine = false;

    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] protected Animator animator;
    protected void Awake()
    {
        animator.speed = 1f / attackCooldown;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f; 
        healthBar.SetMaxHealth(health);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void Update()
    {
        if (target == null)
            return;
        healthBar.SetHealth(health);
        FindPath();
    }

    private void FindPath()
    {
        if ((GetRemainingDistace() <= agent.stoppingDistance) && !pathCompleteCoroutine)
            StartCoroutine(PathComplete());

        float distance = Vector3.Distance(target.position, transform.position);

        if (isFollow || distance <= agroRadius)
        {
            if (!isFollow)
            {
                isFollow = true;
                agent.stoppingDistance = attackRadius;
            }
            agent.SetDestination(target.position);

            if (distance <= attackRadius)
            {
                //Debug.Log("В атаку!");
                if (canAttack)
                {
                    Attack();
                    canAttack = false;
                    StartCoroutine(AttackCooldown());
                }
                FaceTarget();
            }
        }
        else if (pathComplete)
            NavMeshRandomPoint();
    }

    private void NavMeshRandomPoint()
    {
        bool isCorrectPoint = false;
        NavMeshPath path = new NavMeshPath();
        NavMeshHit hit = new NavMeshHit();

        while (!isCorrectPoint)
        {
            if (NavMesh.SamplePosition(patrolZoneCenter.position + Random.insideUnitSphere * randomPointRadius, out hit, randomPointRadius, NavMesh.AllAreas))
            {
                agent.CalculatePath(hit.position, path);

                if (path.status == NavMeshPathStatus.PathComplete)
                    isCorrectPoint = true;
            }
        }

        agent.SetDestination(hit.position);
        pathComplete = false;
    }

    IEnumerator PathComplete()
    {
        pathCompleteCoroutine = true;

        yield return new WaitForSeconds(stopAfterPoint);
        pathComplete = true;

        yield return new WaitForSeconds(1f);
        pathCompleteCoroutine = false;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private float GetRemainingDistace()
    {
        float distance = 0f;
        Vector3[] corners = agent.path.corners;

        if (corners.Length > 2)
        {
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                distance += Vector3.Distance(corners[i], corners[i + 1]);
            }
        }
        else distance = agent.remainingDistance;

        return distance;
    }

    protected virtual void Attack()
    {

    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if (soulPrefab != null)
            Instantiate(soulPrefab, new Vector3(transform.position.x,1.5f,transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
        Gizmos.color = Color.green;
        if (patrolZoneCenter != null) Gizmos.DrawWireSphere(patrolZoneCenter.position, randomPointRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}