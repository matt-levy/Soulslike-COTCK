using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;

    [Header("Movement Settings")]
    private Vector3 moveDirection;
    [SerializeField] private float walkingSpeed = 2;
    [SerializeField] private float runningSpeed = 5;
    [SerializeField] private float sprintingSpeed = 8;
    private Vector3 targetRotationDirection;
    [SerializeField] private float rotationSpeed = 15;
    [SerializeField] private int sprintingStaminaCost = 1;
    private float staminaCostAccumulator = 0;

    [Header("Dodge")]
    private Vector3 rollDirection;
    [SerializeField] private int dodgeStaminaCost = 2;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);

        // When the scene changes, run this logic
        SceneManager.activeSceneChanged += OnSceneChanged;

        this.enabled = false;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // If we load into our world scene, enable player controls
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()) 
        {
            this.enabled = true;
        // Otherwise, disable controls
        // Character will not move while in any menu view
        } 
        else 
        {
            this.enabled = false;
        }
    }

    public void HandleAllMovement() {
        HandleGroundedMovement();
        HandleRotation();
        //Ground Check/ Gravity
        //rolling
    }

    private void GetVerticalAndHorizontalInputs() {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;

        // Clamp the movements for animations
    }

    private void HandleGroundedMovement() 
    {
        if (!player.canMove)
            return;

        GetVerticalAndHorizontalInputs();
        
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (player.isSprinting)
        {
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        }
        else
        {
            if (PlayerInputManager.instance.moveAmount > 0.5f) 
            {
                player.characterController.Move(runningSpeed * Time.deltaTime * moveDirection);
            } 
            else if (PlayerInputManager.instance.moveAmount <= 0.5f) 
            {
                player.characterController.Move(Time.deltaTime * walkingSpeed * moveDirection);
            }
        }

        if (PlayerInputManager.instance.moveAmount > 0.5f) {
            player.characterController.Move(runningSpeed * Time.deltaTime * moveDirection);
        } else if (PlayerInputManager.instance.moveAmount <= 0.5f) {
            player.characterController.Move(Time.deltaTime * walkingSpeed * moveDirection);
        }
    }

    private void HandleRotation() 
    {
        if (player.isDead)
            return;

        if (!player.canRotate)
            return;

        if (player.isLockedOn.Value)
        {
            if (player.isSprinting)
            {
                Vector3 targetDirection = Vector3.zero;
                targetDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
                targetDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
                targetDirection.Normalize();
                targetDirection.y = 0;

                if (targetDirection == Vector3.zero)
                    targetDirection = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = finalRotation;
            }
            else
            {
                if (player.playerCombatManager.currentTarget == null)
                    return;

                Vector3 targetDirection;
                targetDirection = player.playerCombatManager.currentTarget.transform.position - transform.position;
                targetDirection.y = 0;
                targetDirection.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = finalRotation;

            }
        }
        else
        {
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero) 
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            player.isSprinting = false;
        }

        if (player.currentStamina.Value <= 0)
        {
            player.isSprinting = false;
            return;
        }

        if (PlayerInputManager.instance.moveAmount > 0.5) 
        {
            player.isSprinting = true;
        }
        else
        {
            player.isSprinting = false;
        }

        if (player.isSprinting)
        {
            // Casting sprint cost * Time.deltaTime yields values that will round to 0
            // Must accumulate them until they can round to 1 or greater, then subtract
            staminaCostAccumulator += sprintingStaminaCost * Time.deltaTime;
            if (staminaCostAccumulator >= 1)
            {
                int cost = (int)staminaCostAccumulator;
                player.currentStamina.Value -= cost;
                staminaCostAccumulator -= cost;
            }
            
        }

    }
    public void AttemptDodge() 
    {
        if (player.isPerformingAction || player.currentStamina.Value <= 0)
            return;

        if (PlayerInputManager.instance.moveAmount > 0) 
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;

            rollDirection.y = 0;
            rollDirection.Normalize();
            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            // Perform roll animation
            player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true);
        } 
        else 
        {
            player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true);
        }

        player.currentStamina.Value -= dodgeStaminaCost;
        
    }
}
