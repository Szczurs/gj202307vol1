using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset of the camera from the player
    public float smoothTime = 0.2f; // Smoothing time for camera movement

    private Vector3 velocity = Vector3.zero; // Current velocity for SmoothDamp

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = playerTransform.position + offset;

            // Use SmoothDamp to smoothly move the camera towards the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
}
