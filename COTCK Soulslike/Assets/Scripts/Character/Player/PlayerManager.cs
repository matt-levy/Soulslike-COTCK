using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections;
using Unity.VisualScripting;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    
    public FixedString64Bytes characterName;

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
    }

    private void Start()
    {
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

    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        currentCharacterData.characterName = this.characterName.ToString();
        currentCharacterData.xPos = transform.position.x;
        currentCharacterData.yPos = transform.position.y;
        currentCharacterData.zPos = transform.position.z;
    }

    public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        this.characterName = currentCharacterData.characterName;
        Vector3 myPosition = new (currentCharacterData.xPos, currentCharacterData.yPos, currentCharacterData.zPos);
        transform.position = myPosition;
    }
}
