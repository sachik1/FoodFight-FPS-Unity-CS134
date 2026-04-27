using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded;

    // Speed multipliers
    public float forwardSpeedMultiplier = 1.5f;
    public float sideSpeedMultiplier = 1f;
    public float reverseSpeedMultiplier = 0.8f;

    // Movement feel
    public float acceleration = 15f;
    public float deceleration = 20f;

    // Jump feel
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 2f;

    // Mouse look
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public Transform cameraTransform;
    private Vector3 cameraOffset;

    public static bool gameStarted = false;
    

    void Start()
    {
        gameStarted = false;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.drag = 0f;

        if (cameraTransform != null)
            cameraOffset = cameraTransform.position - transform.position;

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        yRotation = transform.eulerAngles.y;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue value)
    {
        if (!PlayerController.gameStarted) return;
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    private void Update()
    {
        if (!PlayerController.gameStarted) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerController.gameStarted) return;

        float currentSpeedMultiplier;
        if (movementY > 0)
            currentSpeedMultiplier = forwardSpeedMultiplier;
        else if (movementY < 0)
            currentSpeedMultiplier = reverseSpeedMultiplier;
        else
            currentSpeedMultiplier = sideSpeedMultiplier;

        Vector3 targetVelocity = (transform.forward * -movementY * currentSpeedMultiplier
                        + transform.right * -movementX * sideSpeedMultiplier) * speed;

        Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 targetHorizontal = new Vector3(targetVelocity.x, 0, targetVelocity.z);

        float lerpSpeed = targetHorizontal.magnitude > 0.01f ? acceleration : deceleration;
        Vector3 smoothed = Vector3.Lerp(currentHorizontal, targetHorizontal, lerpSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector3(smoothed.x, rb.velocity.y, smoothed.z);

        // Better jump gravity
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }

    
}