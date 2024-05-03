using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    public float walkingSpeed = 2.0f; // Speed for walking
    public float runningSpeed = 6.0f; // Speed for running
    float rotationSpeed = 100.0f;
    Animator anim;
    bool isGrounded; // Check if the character is grounded
    public Transform groundCheck; // A transform representing the position of the ground check
    public LayerMask groundMask; // A layer mask to determine what is considered ground for the character
    public FollowCharacterCamera cameraScript; // Reference to the FollowCharacterCamera script

    void Start()
    {
        anim = GetComponent<Animator>();
        cameraScript = FindObjectOfType<FollowCharacterCamera>(); // Automatically find and assign the FollowCharacterCamera script
        if (cameraScript == null)
        {
            Debug.LogError("FollowCharacterCamera script not found in the scene!");
        }
    }

    void Update()
    {
        // Get input for movement
        float translation = Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed);
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Move the character
        Move(translation, rotation);

        // Check if the player is holding both Left Shift and W keys to run
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            SetRunningAnimation();
        }
        else
        {
            SetWalkingAnimation(translation);
        }

        // Check if the character is moving
        if (translation != 0 || rotation != 0)
        {
            // Update the camera movement state
            cameraScript.UpdateCharacterMovementState(true);
        }
        else
        {
            // Update the camera movement state
            cameraScript.UpdateCharacterMovementState(false);
        }
    }

    void Move(float translation, float rotation)
    {
        // Translate forward/backward
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);

        // Rotate left/right
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    void SetWalkingAnimation(float translation)
    {
        // Check if the character is walking
        bool isWalking = Mathf.Abs(translation) > 0;

        // Set animation parameters
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", false);
    }

    void SetRunningAnimation()
    {
        // Set animation parameters
        anim.SetBool("Walking", false);
        anim.SetBool("Running", true);
    }

    void FixedUpdate()
    {
        // Check if the character is grounded using a sphere cast
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
    }
}
