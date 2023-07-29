using System.Collections.Generic;
using UnityEngine;

public class TopViewRadiusAttack : MonoBehaviour
{
    public float attackRadius = 2.0f;  // The attack radius around the player
    public float attackCooldown = 1.0f;  // Cooldown time between attacks
    public int attackDamage = 10;  // Damage dealt to enemies per attack

    private float lastAttackTime;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    // Draw the attack range as a gizmo for debug visualization
    private void OnDrawGizmosSelected()
    {
        // Check if the player is in range of any enemy
        bool playerInRange = false;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= attackRadius)
            {
                playerInRange = true;
                break;
            }
        }

        // Set the Gizmos color based on whether the player is in range or not
        if (playerInRange)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Update()
    {
        // Check if the player presses the Space key to attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        // Check for cooldown before attacking
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Attack all enemies within the radius
            foreach (GameObject enemy in enemiesInRange)
            {
                AttackEnemy(enemy);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belonged to an enemy
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void Attack()
    {
        // Attack all enemies within the radius
        foreach (GameObject enemy in enemiesInRange)
        {
            AttackEnemy(enemy);
        }
    }

    private void AttackEnemy(GameObject enemy)
    {
        // Perform your attack action here (e.g., decrease enemy health)
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }

        // Reset the attack cooldown timer
        lastAttackTime = Time.time;
    }
}
