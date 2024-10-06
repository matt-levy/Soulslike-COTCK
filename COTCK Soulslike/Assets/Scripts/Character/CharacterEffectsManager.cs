using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    // Process instant effects (take damage, heal)

    // Process timed effects (poison, build ups)

    // Process static effects (buffs from armor/weapons)
    
    private CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
    {
        // Take in effect 
        // Process it
        effect.ProcessEffect(character);
    }
}
