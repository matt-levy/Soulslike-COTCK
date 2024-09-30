using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterEffectsManager characterEffectsManager;

    [Header("Status")]
    public bool isDead = false;
    
    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool isGrounded = true;
    public bool applyRootMotion = false;
    public bool isSprinting = false;

    [Header("Stats")]
    public TrackedInt endurance = new(10);
    public TrackedInt currentStamina = new(0);
    public int maxStamina;

    public TrackedInt vitality = new(10);
    public TrackedInt currentHealth = new(0);
    public int maxHealth;


    protected virtual void Awake() 
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
    }

    protected virtual void Update() 
    {

    }

    protected virtual void LateUpdate() {

    }

}
