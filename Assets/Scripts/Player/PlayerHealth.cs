using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    private float nextHealCool = 5f;
    private float healCool = 10f;
    private bool isDead = false;
    private float timeSpend = 0f;
    private float timeSpendToHeal = 0f;
    private float takeDamageCool = 3f;
    [SerializeField] private float nextTakeDamage = 0f;
    public bool isAtacked = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isAtacked)
        {
            if (timeSpend >= healCool)
            {
                isAtacked = false;
                timeSpend += Time.deltaTime;                
            }
        }
        else if (!isAtacked)
        {
            timeSpend = 0f;
            //if (currentHealth == maxHealth)
            //    return;
            //else
            //{
                if (timeSpendToHeal >= nextHealCool)
                {
                    Heal();
                    timeSpendToHeal = 0f;
                }
                else
                    timeSpendToHeal += Time.deltaTime;
            //}
        }
        nextTakeDamage += Time.deltaTime;
        Debug.Log(nextTakeDamage);

    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;
        if (nextTakeDamage >= takeDamageCool)
        {
            currentHealth -= damageAmount;
            isAtacked = true;
            nextTakeDamage = 0f;
        }

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            HandleDeath();
        }
    }

    public void Heal()
    {
        if (isDead)
            return;

        currentHealth += 1;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

    }

    private void HandleDeath()
    {
        // Implement any death behavior here, like playing a death animation, game over logic, etc.
        // For now, we'll simply destroy the player GameObject.
        Destroy(gameObject);
    }
}
