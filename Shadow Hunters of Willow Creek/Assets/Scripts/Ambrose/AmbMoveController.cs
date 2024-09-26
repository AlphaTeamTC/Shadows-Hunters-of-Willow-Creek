using UnityEngine;

public class AmbMoveController : MonoBehaviour
{

    [Header("References")]
    public Transform playerObj;
    private CharacterController characterController;

    [Header("Movement Settings")]
    private float movementSpeed = 5f;
    private float jumpHeight = 1.8f;
    private float gravity = 18f;
    public float verticalVelocity;
    public float rotationSpeed;
    public float slopeForce = 5f;
    public float slopeForceLenght = 1.5f;

    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    // Functions

    // Moves the Character Controller in the direction and strengt
    public void Movement(float speed, Vector3 direction, RaycastHit underThePlayer)
    {
        GroundMovement(speed, direction, underThePlayer);
    }

    // Controls the movement when the player is grounded
    public void GroundMovement(float speed, Vector3 direction, RaycastHit underThePlayer)
    {

        // Movement direction with default speed of the player
        Vector3 movementDirection = direction * speed;

        // Speed of the action (running or walking) is added
        movementDirection *= movementSpeed;

        // Gravity value is added
        movementDirection.y = verticalVelocity;

        // If the player is on a slope, a greater gravity is needed to mantain the player grounded
        if (OnSlope(underThePlayer) && movementDirection.y <= 0)
        {
            movementDirection.y *= slopeForce * speed;
        }

        // The player moves
        characterController.Move(movementDirection * Time.deltaTime);

    }

    // The character rotates on the given direction, usually the forward of the model 
    public void RotateCharacter(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, direction.normalized, rotationSpeed * Time.deltaTime);
        }
    }

    // The character jumps by giving it a vertical positive velocity
    public void JumpVerticalVelocity()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
    }

    // the gravity is calculated based on if the character is grounded or falling
    public void FallVerticalVelocity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -0.4f;
        }
        else
        {
            verticalVelocity = verticalVelocity - gravity * Time.deltaTime;
        }
    }

    // Calculates the normal vector under the player to determine if it is a slope
    public bool OnSlope(RaycastHit hit)
    {
        if (hit.normal != Vector3.up)
        {
            return true;
        }

        return false;
    }


}
