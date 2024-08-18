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
    [SerializeField] private float laneSwitchSpeed = 20;
    [SerializeField] private float jumpForce = 10;
    
    private Rigidbody rb;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private Boolean isJumping = false;
    
    private InputAction rightAction;
    private InputAction leftAction;
    private Lane currentLane = Lane.Middle;
    private MovementDirection movementDirection = MovementDirection.None;
    private float lanePositionOffset = 3.75f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        rightAction = InputSystem.actions.FindAction("Right");
        leftAction = InputSystem.actions.FindAction("Left");
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

        if (movementDirection == MovementDirection.None && leftAction.WasPressedThisFrame() && currentLane != Lane.Left) {
            movementDirection = MovementDirection.Left;
        }
        if (movementDirection == MovementDirection.None && rightAction.WasPressedThisFrame() && currentLane != Lane.Right) {
            movementDirection = MovementDirection.Right;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(setPositionForLane(transform.position + Time.fixedDeltaTime * movementSpeed * new Vector3(0, 0, 1)));
    }

    private Boolean isGroundedCheck() {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private IEnumerator startJumpCooldown() {
        isJumping = true;
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
    }

    private Vector3 setPositionForLane(Vector3 position) {
        if (movementDirection == MovementDirection.Left) {
            if (currentLane == Lane.Right && transform.position.x <= 0) {
                movementDirection = MovementDirection.None;
                currentLane = Lane.Middle;
            }
            if (currentLane == Lane.Middle && transform.position.x <= -lanePositionOffset) {
                movementDirection = MovementDirection.None;
                currentLane = Lane.Left;
            }
        } else if (movementDirection == MovementDirection.Right) {
            if (currentLane == Lane.Left && transform.position.x >= 0) {
                movementDirection = MovementDirection.None;
                currentLane = Lane.Middle;
            }
            if (currentLane == Lane.Middle && transform.position.x >= lanePositionOffset) {
                movementDirection = MovementDirection.None;
                currentLane = Lane.Right;
            }
        }
        
        if (movementDirection == MovementDirection.None) {
            switch (currentLane) {
                case Lane.Left:
                    return new Vector3(-lanePositionOffset, position.y, position.z);
                case Lane.Middle:
                    return new Vector3(0, position.y, position.z);
                case Lane.Right:
                    return new Vector3(lanePositionOffset, position.y, position.z);
                default:
                    throw new Exception("Invalid lane");
            }
        }

        var laneSwitchDirectionModifier = movementDirection == MovementDirection.Left ? -1 : 1;
        return new Vector3(position.x + Time.fixedDeltaTime * laneSwitchSpeed * laneSwitchDirectionModifier, position.y, position.z);
    }
    
    private enum Lane {
        Left,
        Middle,
        Right,
    }

    private enum MovementDirection {
        Left, 
        None,
        Right,
    }
}
