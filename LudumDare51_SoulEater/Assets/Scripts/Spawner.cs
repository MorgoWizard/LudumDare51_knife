using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float delay = 5f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[i], transform.position + new Vector3(Random.Range(0f,radius), 0, Random.Range(0f,radius)), Quaternion.identity);
            enemy.GetComponent<Enemy>().patrolZoneCenter = transform;
            if (i == enemies.Length - 1)
                i = 0;

            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}