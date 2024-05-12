using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    public float walkingSpeed = 2.0f; // Speed for walking
    public float runningSpeed = 6.0f; // Speed for running
    float rotationSpeed = 100.0f;
    Animator anim;
    bool isGrounded; // Check if the character is grounded
    public bool isAlive;
    public Transform groundCheck; // A transform representing the position of the ground check
    public LayerMask groundMask; // A layer mask to determine what is considered ground for the character
    public FollowCharacterCamera cameraScript; // Reference to the FollowCharacterCamera script
    public Transform gameoverui;

    public AudioSource audioSource;

    void Start()
    {
        isAlive = true;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        cameraScript = FindObjectOfType<FollowCharacterCamera>(); // Automatically find and assign the FollowCharacterCamera script
        if (cameraScript == null)
        {
            Debug.LogError("FollowCharacterCamera script not found in the scene!");
        }

    }

    void Update()
    {
        if (!isAlive){
        return;
        }
        // Get input for movement
        float translation = Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed);
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        // Move the character
        if(translation != 0 || rotation != 0) {
            Move(translation, rotation);
        } else  {
            audioSource.Stop();
        }
        

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
        audioSource.pitch = translation == 6 ? 1.5f : translation / 2;
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
        
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

    public void Killed(){
        gameoverui.GameObject().SetActive(true);
        Debug.Log("character is killed");
        isAlive = false;
        transform.position = new Vector3(-66.4f, 4.11f, 144.817f);
        StartCoroutine(revive());
    }

    IEnumerator revive(){
        yield return new WaitForSeconds(1);
        isAlive = true;
        gameoverui.GameObject().SetActive(false);
    }
}
