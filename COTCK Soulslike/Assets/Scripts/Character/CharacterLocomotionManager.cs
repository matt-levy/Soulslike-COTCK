using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    private CharacterManager character;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity; // Force at which characters are pulled up/down (gravity)
    [SerializeField] protected float groundedVelocity = -30; // Force at which characters stick to ground
    [SerializeField] protected float fallStartYVelocity = -5; // Force at which our character begins to fall when ungrounded
    [SerializeField] private float gavityForce = -5.55f;
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake() 
    {   
        character = GetComponent<CharacterManager>();
    }

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Default");
    }

    protected virtual void Update()
    {
        HandleGroundCheck();

        if (character.isGrounded)
        {
            if (yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedVelocity;
            }
        }
        else
        {
            if (!fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer += Time.deltaTime;

            yVelocity.y += gavityForce * Time.deltaTime;
        }

        character.characterController.Move(yVelocity * Time.deltaTime);
    }

    protected void HandleGroundCheck()
    {
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
    }

    // protected void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    // }
}
