using UnityEngine;

public class DeerController : MonoBehaviour
{
    private Animator animator;
    public float roamSpeed = 2f; // Speed at which the deer roams
    public float rotationSpeed = 2f; // Speed at which the deer rotates
    public float changeDirectionInterval = 3f; // Interval to change direction
    private float timer;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    void Start()
    {
        // Get the Animator component attached to the deer character
        animator = GetComponent<Animator>();

        // Get the Rigidbody component attached to the deer character
        rb = GetComponent<Rigidbody>();

        // Ensure the Animator component is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        // Ensure the Rigidbody component is not null
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }
        else
        {
            // Set the Rigidbody to kinematic to prevent it from being affected by physics
            rb.isKinematic = true;
        }

        // Initialize the timer
        timer = changeDirectionInterval;
    }

    void Update()
    {
        // Move the deer forward
        transform.Translate(Vector3.forward * roamSpeed * Time.deltaTime);

        // Rotate the deer randomly
        transform.Rotate(Vector3.up, Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);

        // Update the timer
        timer -= Time.deltaTime;

        // Change direction after the interval
        if (timer <= 0f)
        {
            // Reset the timer
            timer = changeDirectionInterval;

            // Generate a random direction
            Vector3 randomDirection = Random.insideUnitSphere;

            // Set the y component to 0 to ensure the deer moves on the horizontal plane
            randomDirection.y = 0;

            // Rotate the deer to face the new direction
            transform.rotation = Quaternion.LookRotation(randomDirection);
        }

        // Trigger the "Run" animation
        animator.SetBool("Run", true);
    }
}