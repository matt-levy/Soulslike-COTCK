using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Attack")]
public class AttackState : AIState
{
    [SerializeField] private AICharacterAttackAction[] attackList;
    [HideInInspector] public AICharacterAttackAction currentAttack;
    [HideInInspector] public bool willPerformCombo = false;
    
    [Header("State Flags")]
    protected bool hasPerformedAttack = false;
    protected bool hasPerformedCombo = false;

    [Header("Pivot After Attack")]  
    [SerializeField] protected bool pivotAfterAttack = false;

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if (aiCharacter.aiCharacterCombatManager.currentTarget == null)
            return SwitchState(aiCharacter, aiCharacter.idle);

        if (aiCharacter.aiCharacterCombatManager.currentTarget.isDead)
            return SwitchState(aiCharacter, aiCharacter.idle);

        // Rotate towards the target whilst attacking 

        aiCharacter.characterAnimatorManager.UpdateAnimatorMovementParameters(0, 0, false);

        // Perform a combo
        if (willPerformCombo && !hasPerformedCombo)
        {
            if (currentAttack.comboAction != null)
            {
                // If we can combo
                // hasPerformedCombo = true;
                // currentAttack.comboAction.AttemptToPerformAction(aiCharacter);
            }
        }

        if (!hasPerformedAttack)
        {
            if (aiCharacter.aiCharacterCombatManager.actionRecoveryTimer > 0)
                return this;

            if (aiCharacter.isPerformingAction)
                return this;

            PerformAttack(aiCharacter);

            return this;
        }

        if (pivotAfterAttack)
            aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);

        return SwitchState(aiCharacter, aiCharacter.combatStance);

    }

    protected void PerformAttack(AICharacterManager aiCharacter)
    {
        if (aiCharacter == null)
            return;
            
        // Following if statement only relevant to boss attacks
        if (attackList.Length != 0 && aiCharacter.isBoss)
        {
            int randIndex = Random.Range(0, attackList.Length);
            currentAttack = attackList[randIndex];
        }

        hasPerformedAttack = true;
        currentAttack.AttemptToPerformAction(aiCharacter);
        aiCharacter.aiCharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        hasPerformedAttack = false;
        hasPerformedCombo = false;
    }
}
