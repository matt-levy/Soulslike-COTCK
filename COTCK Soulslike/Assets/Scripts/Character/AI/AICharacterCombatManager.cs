using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterCombatManager : CharacterCombatManager
{
    [Header("Target Info")]
    public float distanceFromTarget;
    public float viewableAngle;
    public Vector3 targetsDirection;


    [Header("Detection")]
    [SerializeField] private float detectionRadius = 15f;
    public float minFOV = -35f;
    public float maxFOV = 35f;

    [Header("Action Recovery")]
    public float actionRecoveryTimer = 0;

    [Header("Attack Rotation Speed")]
    public float attackRotationSpeed = 25f;

    protected override void Awake()
    {
        base.Awake();

        lockOnTransform = GetComponentInChildren<LockOnTarget>().transform;
    }

    public void FindTargetViaLineOfSite(AICharacterManager aiCharacter)
    {
       
        if (currentTarget != null)
            return;

        Collider[] colliders =  Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.instance.GetCharacterLayers());

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            if (targetCharacter == null || 
                targetCharacter == aiCharacter || 
                targetCharacter.isDead)
                continue;

            if (WorldUtilityManager.instance.CanIDamageThisTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
            {
                // if potential target is found, it must be in front of us
                Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                float angleOfPotentialTarget = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                if (angleOfPotentialTarget > minFOV && angleOfPotentialTarget < maxFOV)
                {
                    // lastly, check for environmental blockage
                    if (Physics.Linecast(
                        aiCharacter.characterCombatManager.lockOnTransform.position, 
                        targetCharacter.characterCombatManager.lockOnTransform.position, 
                        WorldUtilityManager.instance.GetEnviroLayers()))
                    {
                        Debug.DrawLine(aiCharacter.characterCombatManager.lockOnTransform.position, 
                            targetCharacter.characterCombatManager.lockOnTransform.position);
                    }
                    else
                    {
                        targetsDirection = targetCharacter.transform.position - transform.position;
                        viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, targetsDirection);
                        aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                        PivotTowardsTarget(aiCharacter);
                    }
                }
            }
            
        }
    }

    public void PivotTowardsTarget(AICharacterManager aiCharacter)
    {
        // Play pivot anim based on viewable angle of current target
        if (aiCharacter.isPerformingAction)
            return;

        // Play Animations based on turn angle 
        if (viewableAngle >= 20 && viewableAngle <= 60)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Right_Turn_90", true);
        }
        else if (viewableAngle <= -20 && viewableAngle >= -60)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Left_Turn_90", true);
        }
        else if (viewableAngle >= 61 && viewableAngle <= 110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Right_Turn_180", true);
        }
        else if (viewableAngle <= -61 && viewableAngle >= -110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Left_Turn_180", true);
        }
        
    }

    // private IEnumerator PivotCoroutine(AICharacterManager aiCharacter, float rotationSpeed = 0.2f)
    // {
    //     while (Math.Abs(viewableAngle) > 0.1f)
    //     {
    //         float step = rotationSpeed * Time.deltaTime;

    //         Vector3 direction = aiCharacter.aiCharacterCombatManager.targetsDirection;
    //         direction.y = 0;
    //         Quaternion targetRotation = Quaternion.LookRotation(direction);


    //         aiCharacter.transform.rotation = Quaternion.RotateTowards(aiCharacter.transform.rotation, targetRotation, step);

    //         viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(aiCharacter.transform, direction);

    //         aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Pivot_Test", true, false);

    //         yield return null;
    //     }

    //     aiCharacter.transform.rotation = Quaternion.LookRotation(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position - aiCharacter.transform.position);
    // }

    public void RotateTowardsAgent(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isMoving.Value)
        {
            aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
        }
    }

    public void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacter)
    {
        if (currentTarget == null)
            return;

        if (!aiCharacter.canRotate)
            return;

        if (!aiCharacter.isPerformingAction)
            return;

        Vector3 targetDirection = currentTarget.transform.position - aiCharacter.transform.position;
        targetDirection.y = 0;
        targetDirection.Normalize();

        if (targetDirection == Vector3.zero)
        {
            targetDirection = aiCharacter.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        
        aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetRotation, attackRotationSpeed * Time.deltaTime);

    }

    public void HandleActionRecovery(AICharacterManager aiCharacter)
    {
        if (actionRecoveryTimer > 0)
        {
            if (!aiCharacter.isPerformingAction)
            {
                actionRecoveryTimer -= Time.deltaTime;
            }
        }
    }
}
