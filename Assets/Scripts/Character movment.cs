using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // Movement speed of the player
    public float speed = 5.0f;

    // Sensitivity of the mouse movement
    public float mouseSensitivity = 100.0f;

    // Force with which the player will jump
    public float jumpForce = 5.0f;

    // Rotation around the x-axis (looking up and down)
    private float xRotation = 0f;

    // Reference to the CharacterController component
    private CharacterController controller;

    // Reference to the main camera, for controlling the view
    private Camera cam;

    // Gravity value applied to the player
    private float gravity = -9.81f;

    // Current velocity of the player (used for gravity and jumping)
    private Vector3 velocity;

    // Checks if the player is grounded
    private bool isGrounded;

    void Start()
    {
        // Get the CharacterController component attached to this gameObject
        controller = GetComponent<CharacterController>();

        // Get the main camera from the scene
        cam = Camera.main;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // Reset the y velocity when the player is grounded
            velocity.y = -2f;
        }

        // Read the movement input from keyboard
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement vector based on the player's input and orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the character controller based on calculated vector
        controller.Move(move * speed * Time.deltaTime);

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate the initial velocity needed to achieve the jump height
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the character controller based on the gravity applied
        controller.Move(velocity * Time.deltaTime);

        // Look around with the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate rotation about the x-axis based on mouse input
        xRotation -= mouseY;

        // Clamp the x rotation to prevent flipping the view
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera for looking up and down
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player object to turn left and right
        transform.Rotate(Vector3.up * mouseX);
    }
}