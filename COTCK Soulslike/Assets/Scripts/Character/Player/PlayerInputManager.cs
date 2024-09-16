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

    [Header("Movement Input")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Camera Movement Input")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

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
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()) {
            instance.enabled = true;
        // Otherwise, disable controls
        // Character will not move while in any menu view
        } else {
            instance.enabled = false;
        }
    }

    private void OnEnable() {
        if (playerControls == null) {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDestroy() {
        // if we destroy this object, unsubscribe from this event
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Update() {
        HandleMovementInput();
        HandleCameraMovementInput();
    }

    private void HandleMovementInput() {
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
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

        // If we are locked on, pass vertical and horizontal params
    }

    private void HandleCameraMovementInput() {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }
}
