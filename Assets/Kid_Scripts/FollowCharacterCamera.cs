using UnityEngine;

public class FollowCharacterCamera : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    public float distanceFromCharacter = 3f; // Distance from the character
    public float heightAboveCharacter = 2f; // Height above the character
    public float offsetZ = -5f; // Offset along the character's forward direction
    public float rotationY = 10f; // Y rotation of the camera
    public float playerPositionTargetOffset = 0.5f;

    private Vector3 lastCharacterPosition; // Last recorded position of the character
    private bool isCharacterMoving; // Flag to track if the character is moving

    void Start()
    {
        // Initialize the last character position
        lastCharacterPosition = character.position;
        AdjustCamera();
    }

    void LateUpdate()
    {
        // Check if the character transform is assigned and the character is moving
        if (character != null && isCharacterMoving)
        {
            AdjustCamera();
        }
    }

    // Method to update the character movement state
    public void UpdateCharacterMovementState(bool isMoving)
    {
        isCharacterMoving = isMoving;
    }

    private void AdjustCamera()
    {
        // Calculate the desired position behind, above, and along the character's forward direction
            Vector3 desiredPosition = character.position - character.forward * distanceFromCharacter
                                      + character.up * heightAboveCharacter
                                      + character.forward * offsetZ;

            // Set the camera's position
            transform.position = desiredPosition;

            // Apply Y rotation to the camera
            transform.rotation = Quaternion.Euler(0f, character.eulerAngles.y + rotationY, 0f);

            // Make the camera face the character's position
            transform.LookAt(character.position + (Vector3.up * playerPositionTargetOffset));

            // Update the last character position
            lastCharacterPosition = character.position;
    }
}
