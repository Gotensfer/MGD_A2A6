using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    [SerializeField] InputActionReference jump;
    [SerializeField] Joystick virtualJoystick;

    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float rotationOffsetDegrees = 60f; // Offset angle in degrees

    Vector2 movementInput;
    Vector3 movementDirection;

    [SerializeField] float jumpForce;
    [SerializeField] float movementSpeed;

    bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        jump.action.performed += Jump;
    }

    private void Update()
    {
        HandleMovement();
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        print("Jump!");

        animator.SetTrigger("Jump");

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void HandleMovement()
    {
        movementInput = new Vector2(virtualJoystick.Horizontal, virtualJoystick.Vertical);

        if (movementInput.magnitude > 0)
        {
            isMoving = true;

            animator.SetBool("Walk", true);


            movementDirection = new Vector3(movementInput.x, 0, movementInput.y);


            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            Quaternion offsetRotation = Quaternion.Euler(0, rotationOffsetDegrees, 0);
            Quaternion finalRotation = targetRotation * offsetRotation;

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                finalRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            animator.SetBool("Walk" ,false);
            isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddForce(movementDirection * movementSpeed);
        }
    }
}
