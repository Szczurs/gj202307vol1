using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            HandleDeath();
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead)
            return;

        currentHealth += healAmount;

        // Make sure health doesn't exceed maxHealth
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void HandleDeath()
    {
        // Implement any death behavior here, like playing a death animation, game over logic, etc.
        // For now, we'll simply destroy the player GameObject.
        Destroy(gameObject);
    }
}
