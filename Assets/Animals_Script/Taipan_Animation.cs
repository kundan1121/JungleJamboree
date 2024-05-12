using UnityEngine;

public class TaipanController : MonoBehaviour
{
    private Animator animator;
    private Transform target; // Reference to the player's transform
    private Vector3 startPosition; // Start position of the snake
    public float walkSpeed = 1.5f; // Speed at which the snake walks
    public float runSpeed = 3f; // Speed at which the snake runs
    public float chaseDistance = 10f; // Distance at which the snake starts chasing the player
    public float attackDistance = 1f; // Distance at which the snake attacks the player
    public float walkRadius = 5f; // Maximum radius snake can move around its start position

    [SerializeField]
    private LayerMask targetLayerMask; // Layer mask for targeting the humanoid character

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position; // Save the starting position

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        target = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, targetLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                target = collider.transform;
                break;
            }
        }

        if (target == null) {
            Walk();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
    }

    void Walk()
    {
        if (Vector3.Distance(transform.position, startPosition) > walkRadius)
        {
            // Rotate towards start position if it goes beyond the walk radius
            Vector3 direction = (startPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // Randomly change direction
            if (Random.Range(0, 100) < 10) // 10% chance to change direction
            {
                transform.Rotate(0, Random.Range(-90, 90), 0);
            }
        }

        animator.SetFloat("Speed", walkSpeed);
        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
    }

    void ChasePlayer()
    {
        animator.SetFloat("Speed", runSpeed);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        MyCharacterController characterController = target.GetComponent<MyCharacterController>();
        if (characterController.isAlive){
            target.GetComponent<MyCharacterController>().Killed();
            Debug.Log("Snake attacks the player!");
        }
    }
}
