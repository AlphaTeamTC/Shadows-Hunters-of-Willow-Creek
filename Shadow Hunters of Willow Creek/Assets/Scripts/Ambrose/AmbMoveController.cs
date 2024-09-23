using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbMoveController : MonoBehaviour
{

    [Header("References")]
    public Transform playerObj;
    private CharacterController characterController;

    [Header("Movement Settings")]
    private float movementSpeed = 5f;
    private float jumpHeight = 2.5f;
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
    public void Movement(float speed, Vector3 direction, RaycastHit underThePlayer)
    {
        GroundMovement(speed, direction, underThePlayer);
    }

    public void GroundMovement(float speed, Vector3 direction, RaycastHit underThePlayer)
    {

        Vector3 movementDirection = direction * speed;

        movementDirection *= movementSpeed;

        movementDirection.y = verticalVelocity;


        if (OnSlope(underThePlayer) && movementDirection.y <= 0)
        {
            movementDirection.y *= slopeForce * speed;
        }

        characterController.Move(movementDirection * Time.deltaTime);

    }

    public void RotateCharacter(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, direction.normalized, rotationSpeed * Time.deltaTime);
        }
    }

    
    public void JumpVerticalVelocity()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
    }

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

    public bool OnSlope(RaycastHit hit)
    {
        if (hit.normal != Vector3.up)
        {
            return true;
        }

        return false;
    }
}
