using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Idle")]
public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if (aiCharacter.characterCombatManager.currentTarget != null)
        {
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }
        else
        {
            // return this state to continually search for target
            Debug.Log("We have no target");
            aiCharacter.aiCharacterCombatManager.FindTargetViaLineOfSite(aiCharacter);
            return this;
        }

    }
}
