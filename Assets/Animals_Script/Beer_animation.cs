using UnityEngine;

public class Beer_Animation : MonoBehaviour
{
    private Animator animator;
    private Transform target; // Reference to the player's transform
    public float walkSpeed = 1.5f; // Speed at which the beer walks
    public float runSpeed = 3f; // Speed at which the beer runs
    public float chaseDistance = 10f; // Distance at which the beer starts chasing the player
    public float attackDistance = 2f; // Distance at which the beer attacks the player

    [SerializeField]
    private LayerMask targetLayerMask; // Layer mask for targeting the character

    void Start()
    {
        // Get the Animator component attached to the beer character
        animator = GetComponent<Animator>();

        // Ensure the Animator component is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        // target = null;
        // Find the character within the target layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseDistance, targetLayerMask); // Use chaseDistance as radius
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                target = collider.transform;
                break;
            }
        }
        

        if (target == null) return;

        // Calculate the distance between the beer and the player
        // float distanceToPlayer = Vector3.Distance(transform.position, target.position);
         float distanceToPlayer = (target != null) ? Vector3.Distance(transform.position, target.position) : Mathf.Infinity;

        // If the player is within the chase distance, start chasing
        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            // Otherwise, start walking
            Walk();
        }

        // If the player is within the attack distance, attack
        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
    }

    void Walk()
    {
        // Play walk animation
        animator.SetFloat("Speed", walkSpeed);

        // Move the beer forward
        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
    }

    void ChasePlayer()
    {
        // Play run animation
        animator.SetFloat("Speed", runSpeed);

        // Rotate towards the player
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Move towards the player
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Perform attack logic here
        Debug.Log("Beer attacks the player!");
    }
}
