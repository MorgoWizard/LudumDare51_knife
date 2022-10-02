using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject soulPrefab;

    [SerializeField]private float health = 5;
    
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    private void Update()
    {
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            if (soulPrefab != null)
                Instantiate(soulPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
