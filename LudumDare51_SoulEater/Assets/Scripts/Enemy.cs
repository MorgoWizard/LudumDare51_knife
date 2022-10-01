using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject soulPrefab;

    private int health = 5;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(1);
        }

        if (health <= 0)
        {
            if (soulPrefab != null)
                Instantiate(soulPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
