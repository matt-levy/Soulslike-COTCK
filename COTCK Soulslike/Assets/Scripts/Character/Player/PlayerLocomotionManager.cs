using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;
    private Vector3 moveDirection;
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement() {
        HandleGroundedMovement();
        //Ground Check/ Gravity
        //Rotation
        //rolling
    }

    private void GetVerticalAndHorizontalInputs() {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;

        // Clamp the movements for animations
    }

    private void HandleGroundedMovement() {
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (PlayerInputManager.instance.moveAmount > 0.5f) {
            player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        } else if (PlayerInputManager.instance.moveAmount <= 0.5f) {
            player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }
    }
}
