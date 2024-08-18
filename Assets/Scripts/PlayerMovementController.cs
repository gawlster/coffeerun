using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    
    private Rigidbody rb;
    private InputAction jumpAction;
    private Boolean isJumping = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update() {
        if (!isJumping && isGroundedCheck() && jumpAction.IsPressed()) {
            StartCoroutine(startJumpCooldown());
            rb.AddForce(jumpForce * new Vector3(0, 1, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
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
