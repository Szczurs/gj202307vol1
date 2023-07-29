using UnityEngine;

public class TopViewEnemyMovement : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject
    public float maxSpeed = 6.0f; // Maximum movement speed of the enemy when escaping
    public float minSpeed = 3.0f; // Minimum movement speed of the enemy when escaping
    public float minDistanceToPlayer = 10.0f; // Minimum distance to start running away from the player
    public float rangeAroundPointOfInterest = 20.0f; // Range around the point of interest for random movement

    public float currentDistanceToPlayer { get; private set; } // Public variable to store the current distance
    public float currentSpeed { get; private set; } // Public variable to store the current speed

    public Transform pointOfInterest; // Reference to the point of interest's transform

    private Transform playerTransform; // Reference to the player's transform
    private Vector3 randomDestination; // Random destination for movement when not escaping

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

        // Set the initial random destination around the point of interest
        SetRandomDestination();
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
        else
        {
            // If the enemy is not within the minimum distance, move randomly around the point of interest
            MoveRandomlyAroundPointOfInterest();
        }
    }

    private void MoveRandomlyAroundPointOfInterest()
    {
        // If the enemy has reached the random destination, set a new one
        if (Vector3.Distance(transform.position, randomDestination) <= 0.1f)
        {
            SetRandomDestination();
        }

        // Move the enemy towards the random destination
        transform.position = Vector3.MoveTowards(transform.position, randomDestination, currentSpeed * Time.deltaTime);
    }

    private void SetRandomDestination()
    {
        // Generate a random direction and distance within the specified range
        Vector2 randomDirection = Random.insideUnitCircle.normalized * rangeAroundPointOfInterest;
        Vector3 randomPosition = pointOfInterest.position + new Vector3(randomDirection.x, 0f, randomDirection.y);

        // Set the random destination
        randomDestination = randomPosition;
    }
}
