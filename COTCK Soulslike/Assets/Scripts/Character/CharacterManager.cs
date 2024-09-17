using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController characterController;
    [HideInInspector] public Animator animator;
    
    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool applyRootMotion = false;
    public bool isSprinting = false;


    protected virtual void Awake() {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() {

    }

    protected virtual void LateUpdate() {

    }
}
