using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterEffectsManager characterEffectsManager;
    [HideInInspector] public CharacterCombatManager characterCombatManager;
    [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
    [HideInInspector] public CharacterUIManager characterUIManager;
     
    [Header("Character Group")]
    public CharacterGroup characterGroup;
    public bool isBoss = false;


    [Header("Status")]
    public bool isDead = false;
    
    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool isGrounded = true;
    public bool applyRootMotion = false;
    public bool isSprinting = false;
    public TrackedBool isLockedOn = new(false);
    public TrackedBool isMoving = new(false);
    public bool isUsingRightHand = false;
    public bool isUsingLeftHand = false;
    public bool isInvincible = false;
    public bool hasArrowLoaded = false;
    public TrackedBool isAiming = new(false);

    [Header("Stats")]
    public TrackedInt endurance = new(15);
    public TrackedInt currentStamina;
    public int maxStamina;

    public TrackedInt vitality = new(15);
    public TrackedInt currentHealth;
    public int maxHealth;


    protected virtual void Awake() 
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterUIManager = GetComponent<CharacterUIManager>();
    }

    protected virtual void Start()
    {
        IgnoreMyOwnColliders();

        isMoving.OnValueChanged += OnIsMovingChanged;
    }

    private void OnDestroy()
    {
        isMoving.OnValueChanged -= OnIsMovingChanged;
    }

    protected virtual void Update() 
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void LateUpdate() 
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void IgnoreMyOwnColliders()
    {
        Collider characterControllerCollider = GetComponent<Collider>();
        Collider[] damagableCharacterColliders = GetComponentsInChildren<Collider>();

        List<Collider> ignoreColliders = new List<Collider>();

        // Add all damage colliders to the list
        foreach (var collider in damagableCharacterColliders)
        {
            ignoreColliders.Add(collider);
        }

        // Add the character controller collider to the list
        ignoreColliders.Add(characterControllerCollider);

        // Go through every collider in the list, make them ignore each other
        foreach (var collider in ignoreColliders)
        {
            foreach (var otherCollider in ignoreColliders)
            {
                Physics.IgnoreCollision(collider, otherCollider, true);
            }
        }

    }

    public void OnIsLockOnChanged(bool old, bool isLockedOn)
    {
        if (!isLockedOn)
        {
            characterCombatManager.currentTarget = null;
            // Disable Boss HP Bar
            if (isBoss)
            {
                characterUIManager.BossHPBarToggle(false);
            }
        }

        // Enable Boss HP Bar
        if (isBoss && isLockedOn)
        {
            characterUIManager.BossHPBarToggle(true);
        }
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        currentHealth.Value = 0;
        isDead = true;

        // Reset any flags here that need to be reset
        // Nothing yet

        // if not grounded, play aerial death animation

        characterAnimatorManager.PlayTargetActionAnimation("Dead_01", true);

        // Play some death sfx

        yield return new WaitForSeconds(5);

        // Award players with runes (if ai)

        // disable character
        this.enabled = false;
    }

    public void CheckHP(int oldValue, int newValue)
    {
        if (currentHealth.Value <= 0)
        {
            Debug.Log("Health less than or equal to 0");
            StartCoroutine(ProcessDeathEvent());
        }

        if (currentHealth.Value > maxHealth)
        {
            currentHealth.Value = maxHealth;
        }

        if (newValue < oldValue && newValue != 0)
        {
            characterAnimatorManager.PlayTargetActionAnimation("React_Damage_01", true);
        }
    }

    public void OnIsMovingChanged(bool oldStatus, bool newStatus)
    {
        animator.SetBool("isMoving", isMoving.Value);
    }

    public void SetCharacterActionHand(bool rightHandedAction) 
    {
        if (rightHandedAction)
        {
            isUsingLeftHand = false;
            isUsingRightHand = true;
        }
        else
        {
            isUsingRightHand = false;
            isUsingLeftHand = true;
        }
    }

    public void EnableInvinsibility()
    {
        isInvincible = true;
    }

    public void DisableInvinsibility()
    {
        isInvincible = false;
    }
}
