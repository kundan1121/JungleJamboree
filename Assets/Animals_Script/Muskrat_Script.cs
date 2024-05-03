using UnityEngine;

public class RatController : MonoBehaviour
{
    private Animator animator;
    private bool isRunning = false;
    private Vector3 targetPosition;

    public float roamRadius = 5f; // Radius within which the rat roams
    public float runningSpeed = 5f; // Speed at which the rat moves while running
    public LayerMask playerLayer; // Layer mask for detecting the player

    void Start()
    {
        // Get the Animator component attached to the rat character
        animator = GetComponent<Animator>();

        // Ensure the Animator component is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        // Start with roaming behavior
        TriggerRun();
    }

    void Update()
    {
        // Check the distance from the rat to the player for running behavior
        Collider[] colliders = Physics.OverlapSphere(transform.position, roamRadius, playerLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // The player is within the roam radius, trigger running behavior
                TriggerRun();
                return;
            }
        }
    }

    void TriggerRun()
    {
        isRunning = true;
        animator.SetBool("Run", true);

        // Choose a random target position within the roam radius
        Vector2 randomDirection = Random.insideUnitCircle.normalized * roamRadius;
        targetPosition = transform.position + new Vector3(randomDirection.x, 0f, randomDirection.y);
    }

    void FixedUpdate()
    {
        // Move towards the target position while running
        if (isRunning)
        {
            // Check if the rat has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                // Choose a new random target position
                TriggerRun();
            }
            else
            {
                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, runningSpeed * Time.fixedDeltaTime);

                // Rotate towards the target position
                transform.LookAt(targetPosition);
            }
        }
    }
}
