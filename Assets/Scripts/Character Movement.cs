using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float originalHeight = 1f;
    public float crouchHeight = 0.5f; // Height of the crouched character
    public Animator movementAnimation; // Reference to the Animation component attached to the door

    private Rigidbody rb;
    private bool isGrounded;
    private Camera playerCamera;
    private bool isCrouching = false;
    private Vector3 originalCameraPosition;
    private string moveForwardAnimation = "Run Forward"; // Name of the closing animation
    private string idleAnimation = "Idle"; // Name of the closing animation



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>(); // Assuming the camera is a child of the player GameObject

        originalHeight = rb.transform.localScale.y;
        originalCameraPosition = playerCamera.transform.localPosition;
        // Lock cursor at the start of the game
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            // Moving
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + transform.TransformDirection(movement));
            movementAnimation.Play(moveForwardAnimation);
        }
        else
        {
            // Not moving
            movementAnimation.Play(idleAnimation);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (!isCrouching)
            {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x, crouchHeight, rb.transform.localScale.z);
                isCrouching = true;
            }
            else
            {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x, originalHeight, rb.transform.localScale.z);
                isCrouching = false;
            }
        }

        // Mouse look (for first-person camera)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.Rotate(Vector3.left * mouseY);
    }

   

    void ResetCameraPosition()
    {
        playerCamera.transform.localPosition = originalCameraPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}