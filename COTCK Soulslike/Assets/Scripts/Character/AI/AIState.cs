using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : ScriptableObject
{
    public virtual AIState Tick(AICharacterManager aiCharacter)
    {

        // Do some logic to find the player

        // if we found player, return some new state

        // if we have not, continue to run state


        return this;
    }

    public virtual AIState SwitchState(AICharacterManager aiCharacter, AIState newState)
    {
        ResetStateFlags(aiCharacter);
        return newState;
    }

    protected virtual void ResetStateFlags(AICharacterManager aICharacter)
    {
        // reset any state flags here so when you return to the state, the are blank once again
    }
}
