using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerManager player;
    PlayerControls playerControls;
    
    [Header("Camera Movement Input")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Movement Input")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Player Action Input")]
    [SerializeField] private bool dodgeInput = false;
    [SerializeField] private bool sprintInput = false;
    [SerializeField] private bool RB_Input = false;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);

        // When the scene changes, run this logic
        SceneManager.activeSceneChanged += OnSceneChanged;

        instance.enabled = false;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // If we load into our world scene, enable player controls
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()) 
        {
            instance.enabled = true;
        // Otherwise, disable controls
        // Character will not move while in any menu view
        } 
        else 
        {
            instance.enabled = false;
        }
    }

    private void OnEnable() {
        if (playerControls == null) {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            
            // Holding input sets bool to true, releasing sets bool to false
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            playerControls.PlayerActions.RB.performed += i => RB_Input = true;
        }

        playerControls.Enable();
    }

    private void OnDestroy() {
        // if we destroy this object, unsubscribe from this event
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Update() 
    {   
       HandleAllInputs();
    }

    private void HandleAllInputs() 
    {
        HandleMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprinting();
        HandleRBInput();
    }

    // Movement

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        // Clamp move speed - walk and run
        if (moveAmount <= 0.5 && moveAmount > 0) {
            moveAmount = 0.5f;
        } else if (moveAmount > 0.5 && moveAmount <= 1) {
            moveAmount = 1f;
        }

        if (player == null)
            return;

        // x is 0 because we only strafe when locked on
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

        // If we are locked on, pass vertical and horizontal params
    }

    private void HandleCameraMovementInput() 
    {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }

    // Actions

    private void HandleDodgeInput() 
    {
        if (dodgeInput == true) 
        {
            dodgeInput = false;

            // Future note: don't dodge if menu is open
            player.playerLocomotionManager.AttemptDodge();

        }
    }

    private void HandleSprinting() 
    {
        if (sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.isSprinting = false;
        }
    }

    private void HandleRBInput()
    {
        if (RB_Input)
        {
            RB_Input = false;

            // If we have a UI window open, return and do nothing

            player.SetCharacterActionHand(true);

            // If we are two handing weapon, run two handed action

            player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RB_Action, player.playerInventoryManager.currentRightHandWeapon);
        }
    }
}
