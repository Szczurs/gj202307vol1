using UnityEngine;

public class TopViewEnemyMovement : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject
    public float maxSpeed = 6.0f; // Maximum movement speed of the enemy
    public float minSpeed = 3.0f; // Minimum movement speed of the enemy
    public float minDistanceToPlayer = 10.0f; // Minimum distance to start running away from the player

    public float currentDistanceToPlayer { get; private set; } // Public variable to store the current distance
    public float currentSpeed { get; private set; } // Public variable to store the current speed

    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // Find the player GameObject based on the tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        // Check if the player GameObject was found
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found with tag: " + playerTag);
        }
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return; // Exit the update if the player is not found
        }

        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Calculate the distance between the enemy and the player
        currentDistanceToPlayer = directionToPlayer.magnitude;

        // Calculate the normalized distance (a value between 0 and 1)
        float normalizedDistance = Mathf.Clamp01(currentDistanceToPlayer / minDistanceToPlayer);

        // Calculate the adjusted speed based on the normalized distance (inverse relationship)
        currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, normalizedDistance);

        // Determine if the player is within the minimum distance to run away
        if (currentDistanceToPlayer <= minDistanceToPlayer)
        {
            // Calculate the target position that is the minimum distance away from the player
            Vector3 targetPosition = playerTransform.position - directionToPlayer.normalized * minDistanceToPlayer;

            // Move the enemy towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
        }
    }
}
