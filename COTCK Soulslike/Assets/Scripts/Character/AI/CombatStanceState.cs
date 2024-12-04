using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI/States/CombatStance")]
public class CombatStanceState : AIState
{
    [Header("Attacks")]
    public List<AICharacterAttackAction> aiCharacterAttacks; // All possible attacks this character can do
    protected List<AICharacterAttackAction> potentialAttacks; // All attacks possible in this situation (angle, distance, etc.)
    private AICharacterAttackAction chosenAttack;
    private AICharacterAttackAction previousAttack;
    protected bool hasAttack = false;

    [Header("Combo")]
    [SerializeField] protected bool canPerformCombo = false; // If this character can perform a combo after initial attack
    [SerializeField] protected int chanceToPerformCombo = 25; // Chance in % to perform combo on next attack
    protected bool hasRolledForComboChance = false; // If we have already rolled for combo during this state

    [Header("Engagement Distance")]
    [SerializeField] public float maxEngagementDist = 5f; // Dist we have to be away from target to change to pursue state

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isPerformingAction)
            return this;
        
        if (!aiCharacter.navMeshAgent.enabled)
            aiCharacter.navMeshAgent.enabled = true;
        
        // If you want the ai character to face and turn towards its target when youre outside its fov
        // Implenetation here...
        if (!aiCharacter.isMoving.Value)
        {
            if (aiCharacter.aiCharacterCombatManager.viewableAngle < -30 || aiCharacter.aiCharacterCombatManager.viewableAngle > 30)
            {
                aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }
        }


        aiCharacter.aiCharacterCombatManager.RotateTowardsAgent(aiCharacter);

        // if target no longer there, return to idle state
        if (aiCharacter.aiCharacterCombatManager.currentTarget == null)
            return SwitchState(aiCharacter, aiCharacter.idle);

        if (!hasAttack)
        {
            GetNewAttack(aiCharacter);
        }
        else
        {
            aiCharacter.attack.currentAttack = chosenAttack;
            // roll for combo chance
            return SwitchState(aiCharacter, aiCharacter.attack);


        }

        // if outside combat engagement dist, switch to pursue state
        if (aiCharacter.aiCharacterCombatManager.distanceFromTarget > maxEngagementDist)
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);   

        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        return this; 

    }

    protected virtual void GetNewAttack(AICharacterManager aiCharacter)
    {
        potentialAttacks = new List<AICharacterAttackAction>();

        foreach (var potentialAttack in aiCharacterAttacks)
        {
            // If we are too close, check next attacjk
            if (potentialAttack.minAttackDist > aiCharacter.aiCharacterCombatManager.distanceFromTarget)
                continue;
            // If we are too far, check next attack
            if (potentialAttack.maxAttackDist < aiCharacter.aiCharacterCombatManager.distanceFromTarget)
                continue;
            
            // Check fov for same reasons as above (not implemented)

            potentialAttacks.Add(potentialAttack);
            
        }

        if (potentialAttacks.Count <= 0)
            return;

        var totalWeight = 0;

        foreach (var attack in potentialAttacks)
        {
            totalWeight += attack.attackWeight;
        }

        var randomWeightValue = Random.Range(1, totalWeight + 1);
        var processedWeight = 0;

        foreach (var attack in potentialAttacks)
        {
            processedWeight += attack.attackWeight;

            if (randomWeightValue <= processedWeight)
            {
                chosenAttack = attack;
                previousAttack = chosenAttack;
                hasAttack = true;
                return;
            }
        }
    }

    protected virtual bool RollForOutcomeChance(int outcomeChance)
    {
        bool outcomeWillBePerformed = false;

        int randPercent = Random.Range(0,100);

        if (randPercent < outcomeChance)
            outcomeWillBePerformed = true;
    

        return outcomeWillBePerformed;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        hasRolledForComboChance = false;
        hasAttack = false;
    }


}
