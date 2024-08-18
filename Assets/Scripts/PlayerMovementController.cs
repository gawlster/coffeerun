using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 15;
    [SerializeField] private float crouchSpeed = 10;
    private float movementSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    
    private Rigidbody rb;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private Boolean isJumping = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
    }

    private void Update() {
        if (!isJumping && isGroundedCheck() && jumpAction.IsPressed()) {
            StartCoroutine(startJumpCooldown());
            rb.AddForce(jumpForce * new Vector3(0, 1, 0), ForceMode.Impulse);
        }

        if (isGroundedCheck() && crouchAction.IsPressed()) {
            transform.localScale = new Vector3(1, 0.5f, 1);
            movementSpeed = crouchSpeed;
        } else {
            transform.localScale = new Vector3(1, 1, 1);
            movementSpeed = normalSpeed;
        }
    }

    private void FixedUpdate() {
        Debug.Log(movementSpeed);
        rb.MovePosition(transform.position + Time.deltaTime * movementSpeed * new Vector3(0, 0, 1));
    }

    private Boolean isGroundedCheck() {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private IEnumerator startJumpCooldown() {
        isJumping = true;
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
    }
}
