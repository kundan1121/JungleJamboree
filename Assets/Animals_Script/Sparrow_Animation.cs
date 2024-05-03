using UnityEngine;

public class SparrowController : MonoBehaviour
{
    private Animator animator;
    private bool isFlying = false;
    private Vector3 targetPosition;
    private float flyingTimer = 0f; // Timer to control how long the bird flies
    public float maxFlyingTime = 5f; // Maximum time the bird can fly before landing
    public float flyingSpeed = 5f; // Speed at which the sparrow flies
    public float flyingHeight = 10f; // Desired height at which the sparrow flies
    public float fleeDistance = 5f; // Distance at which the sparrow flees from the player
    public float playerDetectionRadius = 5f; // Radius for detecting the player
    public LayerMask playerLayer; // Layer mask for detecting the player

    // Variables to introduce randomness to flee behavior
    public float fleeRandomRange = 2f; // Random range added to flee distance
    private float fleeRandomOffset; // Random offset for each sparrow

    void Start()
    {
        // Get the Animator component attached to the sparrow character
        animator = GetComponent<Animator>();

        // Ensure the Animator component is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        // Start flying randomly when the game begins
        SetRandomDestination();

        // Initialize flee random offset
        fleeRandomOffset = Random.Range(0f, 360f);
    }

    void Update()
    {
        // Check if the sparrow is flying
        if (isFlying)
        {
            // Calculate the direction to the target position
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;

            // Rotate the sparrow to face the direction of movement
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
            }

            // Move the sparrow towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, flyingSpeed * Time.deltaTime);

            // Keep the sparrow at the desired flying height
            Vector3 newPosition = transform.position;
            newPosition.y = flyingHeight;
            transform.position = newPosition;

            // Increment the flying timer
            flyingTimer += Time.deltaTime;

            // Check if the flying timer exceeds the maximum flying time
            if (flyingTimer >= maxFlyingTime)
            {
                // Stop flying and land on the ground or an environment object
                Land();
            }
        }
    }

   void SetRandomDestination()
{
    // Generate a random direction
    float randomAngle = Random.Range(0f, 360f);
    Vector3 randomDirection = Quaternion.Euler(0f, randomAngle + fleeRandomOffset, 0f) * Vector3.forward;

    // Set the y component to 0 to ensure the sparrow moves on the horizontal plane
    randomDirection.y = 0;

    // Calculate the target position within a radius based on fleeDistance
    float randomDistance = Random.Range(fleeDistance - fleeRandomRange, fleeDistance + fleeRandomRange);
    targetPosition = transform.position + randomDirection.normalized * randomDistance;

    // Ensure the target position is within the bounds of the play area
    targetPosition.x = Mathf.Clamp(targetPosition.x, -10f, 10f);
    targetPosition.z = Mathf.Clamp(targetPosition.z, -10f, 10f);

    // Start flying towards the target position
    isFlying = true;

    // Reset the flying timer
    flyingTimer = 0f;
}


    void Land()
    {
        // Determine the landing position (e.g., ground or tree)
        Vector3 landingPosition = targetPosition;

        // Land at the landing position
        transform.position = landingPosition;

        // Stop flying
        isFlying = false;

        // Resume flying after landing
        SetRandomDestination();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the sparrow collided with the player
        if (other.CompareTag("Player"))
        {
            // Set a new random destination to flee from the player
            SetRandomDestination();
        }
    }

    void FixedUpdate()
    {
        // Check if the player is within the detection radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, playerDetectionRadius, playerLayer);
        bool playerDetected = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Set the target position away from the player
                Vector3 directionToPlayer = transform.position - collider.transform.position;
                targetPosition = transform.position + directionToPlayer.normalized * fleeDistance;

                // Start flying towards the new target position
                isFlying = true;

                // Indicate that the player is detected
                playerDetected = true;
                break;
            }
        }

        // If the player is not detected, continue roaming
        if (!playerDetected && !isFlying)
        {
            // Start flying randomly when not fleeing from the player
            SetRandomDestination();
        }
    }
}
