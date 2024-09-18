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
    private float jumpHeight = 1f;
    private float gravity = 9.81f;
    private float verticalVelocity;
    public float rotationSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Functions
    public void Movement(float speed, Vector3 direction)
    {
        GroundMovement(speed, direction);
    }

    public void GroundMovement(float speed, Vector3 direction)
    {

        Vector3 movementDirection = direction * speed;

        movementDirection *= movementSpeed;

        movementDirection.y = verticalVelocity;

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
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity = verticalVelocity - gravity * Time.deltaTime;
        }
    }
}
