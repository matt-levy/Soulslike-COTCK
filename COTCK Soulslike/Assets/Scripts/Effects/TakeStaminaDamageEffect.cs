using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterEffects/Instant Effects/Take Stamina Damage Effect")]

public class TakeStaminaDamageEffect : InstantCharacterEffect
{
    public int staminaDamage;

    public override void ProcessEffect(CharacterManager character)
    {
        CalculateStaminaDamage(character);
    }

    private void CalculateStaminaDamage(CharacterManager character)
    {
        // Compare the base stamina damage against other effects/modifiers
        // Change value before subtracting/adding
        // Play sound effect/vfx

        character.currentStamina.Value -= staminaDamage;
    }
}
