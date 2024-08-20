using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour {
    [SerializeField] private Mesh walkMesh;
    [SerializeField] private Mesh crouchMesh;
    private MeshFilter meshFilter;
    private BoxCollider collider;
    private Vector3 walkingColliderCenter = new Vector3(0f, 0.08f, 0f);
    private Vector3 walkingColliderSize = new Vector3(0.13f, 0.18f, 0.13f);
    private Vector3 crouchingColliderCenter = Vector3.zero;
    private Vector3 crouchingColliderSize = new Vector3(0.13f, 0.05f, 0.13f);
    [SerializeField] private float normalSpeed = 15;
    [SerializeField] private float crouchSpeed = 15;
    private float movementSpeed = 10;
    [SerializeField] private float laneSwitchSpeed = 20;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float normalGravity = 15;
    [SerializeField] private float fastFallGravity = 40;
    
    private Rigidbody rb;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private Boolean isJumping = false;
    
    private InputAction rightAction;
    private InputAction leftAction;
    private Lane currentLane = Lane.Middle;
    private MovementDirection movementDirection = MovementDirection.None;
    private float lanePositionOffset = 3.75f;
    
    private Boolean isFastFalling = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        meshFilter = GetComponent<MeshFilter>();
        collider = GetComponent<BoxCollider>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        rightAction = InputSystem.actions.FindAction("Right");
        leftAction = InputSystem.actions.FindAction("Left");
    }

    private void Update() {
        if (jumpAction.IsPressed()) {
            Debug.Log(isGroundedCheck());
        }
        if (!isJumping && isGroundedCheck() && jumpAction.IsPressed()) {
            StartCoroutine(startJumpCooldown());
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }

        if (isGroundedCheck() && crouchAction.IsPressed()) {
            meshFilter.mesh = crouchMesh;
            collider.center = crouchingColliderCenter;
            collider.size = crouchingColliderSize;
            movementSpeed = crouchSpeed;
        } else {
            meshFilter.mesh = walkMesh;
            collider.center = walkingColliderCenter;
            collider.size = walkingColliderSize;
            movementSpeed = normalSpeed;
        }

        if (!isGroundedCheck() && crouchAction.IsPressed()) {
            isFastFalling = true;
        } else {
            isFastFalling = false;
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
        rb.AddForce((isFastFalling ? fastFallGravity : normalGravity) * Vector3.down, ForceMode.Acceleration);
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
