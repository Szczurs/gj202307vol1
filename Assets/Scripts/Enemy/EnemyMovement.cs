using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopViewEnemyMovement : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject
    public float maxSpeed = 6.0f; // Maximum movement speed of the enemy when escaping
    public float minSpeed = 3.0f; // Minimum movement speed of the enemy when escaping
    public float minDistanceToPlayer = 5.0f; // Minimum distance to start running away from the player
    public float rangeAroundPointOfInterest = 20.0f; // Range around the point of interest for random movement
    public float stopChasingDistance = 15.0f; // The distance at which the enemy stops chasing and starts random movement

    public float currentDistanceToPlayer = 0; // Public variable to store the current distance
    public float currentSpeed = 0; // Public variable to store the current speed

    public Transform pointOfInterest; // Reference to the point of interest's transform

    public Transform playerTransform; // Reference to the player's transform
    [SerializeField] private Vector3 randomDestination; // Random destination for movement
    [SerializeField] private bool isChasing = false; // Flag to track if the enemy is chasing the player

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

        // Start the coroutine to set new random destinations
        StartCoroutine(SetNewRandomDestinationCoroutine());
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
        currentDistanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);

        // Calculate the normalized distance (a value between 0 and 1)
        float normalizedDistance = Mathf.Clamp01(currentDistanceToPlayer / minDistanceToPlayer);

        // Calculate the adjusted speed based on the normalized distance (inverse relationship)
        currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, normalizedDistance);

        // Determine if the player is within the minimum distance to run away
        if (currentDistanceToPlayer <= stopChasingDistance)
        {
            // Calculate the target position that is away from the player
            Vector3 targetPosition = transform.position - directionToPlayer.normalized * (stopChasingDistance + 2);

            // Move the enemy away from the player
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            // Set chasing flag to true when the player is close
            isChasing = true;
        }
        else
        {
            // If the enemy is not within the minimum distance, check if it is chasing
            if (isChasing)
            {
                // If the enemy is currently chasing, check if the player is beyond the stopChasingDistance
                if (currentDistanceToPlayer >= stopChasingDistance)
                {
                    isChasing = false; // Stop chasing if the player is far enough away                  
                    SetRandomDestination(); // Set a new random destination
                    MoveRandomlyWithinRange(); // Start moving randomly within the range
                }
                else
                {
                    // Continue chasing the player
                    Vector3 targetPosition = playerTransform.position - directionToPlayer.normalized * (stopChasingDistance + 2);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
                    currentSpeed = minSpeed;
                }
            }
            else
            {
                // If the enemy is not chasing, move randomly within the range
                MoveRandomlyWithinRange();
            }
        }
    }


    private void MoveRandomlyWithinRange()
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
        Vector3 randomPosition = pointOfInterest.position + new Vector3(randomDirection.x, randomDirection.y, 0f);

        // Set the random destination with a Z value of 0
        randomDestination = new Vector3(randomPosition.x, randomPosition.y, 0f);
    }

    private IEnumerator SetNewRandomDestinationCoroutine()
    {
        while (true)
        {
            // Set a new random destination
            SetRandomDestination();

            // Wait for 10 seconds before setting the next random destination
            yield return new WaitForSeconds(10f);
        }
    }
}
