using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private PointSystem points;
    public int maxHealth = 100;  // Maximum health of the enemy
    public int currentHealth;   // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth;  // Set the initial health to the maximum health
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the enemy's health is below zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform any death actions here (e.g., play death animation, drop items, etc.)
        points.score += 5;
        Destroy(gameObject);  // Destroy the enemy GameObject when it dies
    }
}