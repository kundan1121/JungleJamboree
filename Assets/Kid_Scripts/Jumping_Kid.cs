using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float jumpForce = 5f; // The force with which the character jumps
    public float gravityMultiplier = 2f; // Multiplier for gravity while jumping
    bool isGrounded = true; // Check if the character is currently grounded
    [HideInInspector]
    public bool isJumping = false; // Check if the character is currently jumping
    Animator anim;
    Rigidbody rb;

    public AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // Ensure Rigidbody has gravity enabled
        rb.useGravity = true;
        // Set collision detection mode to Continuous Dynamic
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed and the character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {   

            // Trigger the jump animation
            
            // Apply the jump force
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            // Set isJumping to true to prevent multiple jumps
            isJumping = true;
            // Set isGrounded to false since the character is jumping
            isGrounded = false;
            anim.SetBool("IsGrounded", isGrounded);
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        // Apply additional gravity while jumping to simulate a more realistic jump arc
        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the character has collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset Rigidbody velocity
            rb.velocity = Vector3.zero;
            // Reset isJumping flag when grounded
            isJumping = false;
            // Set isGrounded to true when grounded
            isGrounded = true;
            anim.SetBool("IsGrounded", isGrounded);
        }
    }
}
