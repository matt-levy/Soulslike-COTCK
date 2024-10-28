using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterCombatManager : CharacterCombatManager
{
    [Header("Target Info")]
    public float distanceFromTarget;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float minDetectionAngle = -35f;
    [SerializeField] private float maxDetectionAngle = 35f;

    [Header("Action Recovery")]
    public float actionRecoveryTimer = 0;

    [Header("Attack Rotation Speed")]
    public float attackRotationSpeed = 25f;

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
                float viewableAngle = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                if (viewableAngle > minDetectionAngle && viewableAngle < maxDetectionAngle)
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
                        aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                    }
                }
            }
            
        }
    }

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
