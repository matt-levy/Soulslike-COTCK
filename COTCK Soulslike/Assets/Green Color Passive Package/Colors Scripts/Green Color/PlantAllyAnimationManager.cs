using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAllyAnimationManager : MonoBehaviour
{
    private Animator animator;
    public AnimatorClipInfo[] currentClips;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    public void PlayWalkAnimation()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    public void PlayAttackAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }

    public float GetAttackAnimationDuration()
    {
        // Assumes the attack animation clip is named "Attack"
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "metarig.001|attack")
            {
                return clip.length;
            }
        }

        // Default duration if the animation is not found
        return 1.0f;
    }
}
