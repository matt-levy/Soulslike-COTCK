using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public void UpdateAnimatorMovementParameters(float horizontal, float vertical) 
    {
        character.animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(
        string targetAnimation, 
        bool isPerformingAction, 
        bool applyRootMotion = true, 
        bool canRotate = false, 
        bool canMove = false) 
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        // Can be used to stop character from attempting a new action
        // Ex. If you are damaged and you try to perform an action, 
        // this flag will be true if you are stunned.
        // Check this before attempting new actions
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;
    }
}