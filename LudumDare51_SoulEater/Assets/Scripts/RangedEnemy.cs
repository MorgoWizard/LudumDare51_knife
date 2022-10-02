using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy
{
    [SerializeField] private Transform progectileSpawner;
    
    [SerializeField] private float angleInDegrees;
    [SerializeField] private GameObject projectile;

    private float g = Physics.gravity.y;

    private void Start()
    {
        progectileSpawner.localEulerAngles = new Vector3(0f, 0f, -angleInDegrees);
    }

    protected override void Attack()
    {
        Vector3 direction = target.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        float x = directionXZ.magnitude;
        float y = direction.y;

        float angleInRadians = angleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject newProjectile = Instantiate(projectile, progectileSpawner.position, Quaternion.identity);
        newProjectile.transform.rotation = progectileSpawner.rotation;
        newProjectile.GetComponent<Rigidbody>().velocity = progectileSpawner.up * v;
    }
}
