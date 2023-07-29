using UnityEngine;

public class TopDown2DMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool faceDirectionOfMovement = true; // Add a bool property to enable/disable facing direction

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput).normalized;

        rb2D.velocity = movementDirection * moveSpeed;

        if (faceDirectionOfMovement && movementDirection.magnitude != 0)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
