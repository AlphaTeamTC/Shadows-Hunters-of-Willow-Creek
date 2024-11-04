using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    private float cameraRotationX = 0f;

    void Update()
    {
        // Get input for movement (WASD)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Move the player character
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Get input for mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally based on mouse X input
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically based on mouse Y input
        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f); // Limit vertical rotation
        playerCamera.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
    }
}
