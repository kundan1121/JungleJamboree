using UnityEngine;

public class ForwardJumpController : MonoBehaviour
{
    public float jumpSpeed = 1f; // Adjust this value to change the jump distance
    public float forwardJumpForce = 1f; // Adjust this value to change the forward jump distance
    public Animator anim; // Reference to the Animator component
    Rigidbody rb;
    bool isRunning = false; // Check if the character is currently running
    bool isJumping = false; // Check if the character is currently jumping
    bool isGrounded = true; // Check if the character is currently grounded

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the character is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Check if the character is running and grounded
        if (isGrounded)
        {
            isRunning = anim.GetBool("Running");
        }
        else
        {
            isRunning = false; // Character can't run while jumping
            anim.SetBool("Running", false); // Ensure the run animation is stopped
        }

        // Check if the spacebar is pressed, the character is running, and not currently jumping
        if (Input.GetKeyDown(KeyCode.Space) && isRunning && !isJumping)
        {
            // Trigger the jump animation
            if (Input.GetAxis("Vertical") > 0) // Check if character is moving forward
            {
                // Play forward jump animation
                anim.SetTrigger("ForwardJump");

                // Calculate the jump velocity
                Vector3 jumpVelocity = transform.up * jumpSpeed;

                // Apply the jump velocity
                rb.velocity = jumpVelocity;

                // Apply a forward force
                Vector3 forwardForce = transform.forward * forwardJumpForce;
                rb.AddForce(forwardForce, ForceMode.Impulse);

                // Set isJumping to true to prevent multiple jumps
                isJumping = true;
            }
            else
            {
                // Play regular jump animation
                anim.SetTrigger("Jump");

                // Calculate the jump velocity
                Vector3 jumpVelocity = transform.up * jumpSpeed;

                // Apply the jump velocity
                rb.velocity = jumpVelocity;

                // Set isJumping to true to prevent multiple jumps
                isJumping = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the character has collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset isJumping flag when grounded
            isJumping = false;
        }
    }
}
