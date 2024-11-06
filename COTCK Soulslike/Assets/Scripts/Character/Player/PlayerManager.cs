using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    
    public FixedString64Bytes characterName;


    protected override void Awake()
    {
        base.Awake();

        // Do more stuff for player functionality
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();

        PlayerCamera.instance.player = this;
        PlayerInputManager.instance.player = this;
        WorldSaveGameManager.instance.player = this;    


    }


    protected override void Start()
    {
        base.Start();

        SetNewMaxHealthValue(0, vitality.Value);
        SetNewMaxStaminaValue(0, endurance.Value);


        // Update our max health/stamina when leveling stats
        this.vitality.OnValueChanged += SetNewMaxHealthValue;
        this.endurance.OnValueChanged += SetNewMaxStaminaValue;


        // Updates our stamina/health bars when a stat changes
        this.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
        this.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;

        this.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;

        this.isLockedOn.OnValueChanged += OnIsLockOnChanged;

        currentHealth.OnValueChanged += CheckHP;
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

        currentCharacterData.currentHealth = this.currentHealth.Value;
        currentCharacterData.currentStamina = this.currentStamina.Value;
        

        currentCharacterData.vitality = this.vitality.Value;
        currentCharacterData.endurance = this.endurance.Value;
    }

    public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        this.characterName = currentCharacterData.characterName;
        Vector3 myPosition = new (currentCharacterData.xPos, currentCharacterData.yPos, currentCharacterData.zPos);
        transform.position = myPosition;

        this.vitality.Value = currentCharacterData.vitality;
        this.endurance.Value = currentCharacterData.endurance;

        this.maxStamina = playerStatsManager.CalculateTotalStaminaBasedOnLevel(this.endurance.Value);
        this.currentStamina.Value = currentCharacterData.currentStamina;
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(this.maxStamina);

        this.maxHealth = playerStatsManager.CalculateTotalHealthBasedOnLevel(this.vitality.Value);
        this.currentHealth.Value = currentCharacterData.currentHealth;
        PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(this.maxHealth);

    }

    private void SetNewMaxHealthValue(int oldVitality, int newVitality)
    {
        maxHealth = playerStatsManager.CalculateTotalHealthBasedOnLevel(newVitality);
        PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth);
        currentHealth.Value = playerStatsManager.CalculateTotalHealthBasedOnLevel(newVitality);
    }

    private void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    {
        maxStamina = playerStatsManager.CalculateTotalStaminaBasedOnLevel(newEndurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(maxStamina);
        currentStamina.Value = playerStatsManager.CalculateTotalStaminaBasedOnLevel(newEndurance);
    }
}
