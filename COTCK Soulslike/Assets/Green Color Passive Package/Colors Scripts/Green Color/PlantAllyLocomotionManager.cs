using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAllyLocomotionManager : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    public float rotationSpeed = 10f;

    private float gravity = -9.81f;

    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        // Get direction to target
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        // Rotate towards target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards target
        Vector3 move = direction * movementSpeed * Time.deltaTime;

        // Apply gravity
        if (characterController.isGrounded)
        {
            verticalVelocity = 0;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity * Time.deltaTime;

        // Move the character
        characterController.Move(move);
    }

    public void StopMovement()
    {
        characterController.Move(Vector3.zero);
    }

    public void LookAtTarget(Vector3 targetPosition)
    {
        // Get direction to target
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        // Rotate ally to face the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
