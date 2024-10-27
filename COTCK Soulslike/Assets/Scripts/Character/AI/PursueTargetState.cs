using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI/States/PursueTarget")]
public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // Check if we are performing an action (if so, do nothing until action is complete)
        if (aiCharacter.isPerformingAction)
            return this;

        // Check if target is null, if we dont have one, return to idle
        if (aiCharacter.aiCharacterCombatManager.currentTarget == null)
            return SwitchState(aiCharacter, aiCharacter.idle);

        // Make sure our navmesh agent is active, if its not, enable it
        if (!aiCharacter.navMeshAgent.enabled)
            aiCharacter.navMeshAgent.enabled = true;

        aiCharacter.aICharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

        // If we are within combat range of target, switch to combat state

        // If target is not reachable, and they are far away, return home

        // pursue target!

        // Option 1 - More performant, might not work on complex terrain
        //aiCharacter.navMeshAgent.SetDestination(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position);

        // Option 2 - Might be better for complex terrain, less performant than option 1
        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        return this;
    }
}
