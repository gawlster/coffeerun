using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rb.MovePosition(transform.position + Time.deltaTime * movementSpeed * new Vector3(0, 0, 1));
    }
}
