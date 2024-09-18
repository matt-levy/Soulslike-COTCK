using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    protected override void Awake()
    {
        base.Awake();

        // Do more stuff for player functionality
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();

        // Give the player the camera
        PlayerCamera.instance.player = this;
        PlayerInputManager.instance.player = this;
        this.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
        this.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;

        // This will be moved when saving and loading is added
        this.maxStamina = playerStatsManager.CalculateTotalStaminaBasedOnLevel(this.endurance);
        this.currentStamina.Value = maxStamina;
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(this.maxStamina);
    }

    protected override void Update()
    {
        base.Update();

        playerLocomotionManager.HandleAllMovement();
        playerStatsManager.RegenStamina();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }
}
