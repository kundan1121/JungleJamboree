using UnityEngine;

public class ColobusController : MonoBehaviour
{
    private Animator animator;
    private bool isRolling = false;
    public float roamSpeed = 2f; // Speed at which the monkeys roam
    public float rotationSpeed = 2f; // Speed at which the monkeys rotate
    public float changeDirectionInterval = 3f; // Interval to change direction
    public float rollDistance = 5f; // Distance at which the monkey rolls
    public LayerMask playerLayer; // Layer mask for detecting the player
    private float timer;

    void Start()
    {
        // Get the Animator component attached to the monkey character
        animator = GetComponent<Animator>();

        // Ensure the Animator component is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        // Initialize the timer
        timer = changeDirectionInterval;
    }

    void Update()
    {
        // Check the distance from the monkey to the player for rolling behavior
        Collider[] colliders = Physics.OverlapSphere(transform.position, rollDistance, playerLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // The player is within the roll distance, trigger rolling behavior
                TriggerRoll();
                return;
            }
        }

        // If the player is not within the roll distance, revert to running behavior
        if (isRolling)
        {
            isRolling = false;
            animator.SetBool("Roll", false);
        }

        // Move the monkey forward
        transform.Translate(Vector3.forward * roamSpeed * Time.deltaTime);

        // Rotate the monkey randomly
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

            // Set the y component to 0 to ensure the monkey moves on the horizontal plane
            randomDirection.y = 0;

            // Rotate the monkey to face the new direction
            transform.rotation = Quaternion.LookRotation(randomDirection);
        }
    }

    void TriggerRoll()
    {
        if (!isRolling)
        {
            isRolling = true;
            animator.SetBool("Roll", true);
        }
    }
}
