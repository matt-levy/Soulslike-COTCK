using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterEffects/Instant Effects/Take Damage Effect")]

public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // If damage is caused by another character, store character here

    [Header("Damage")]
    // Add any other damage types we want in future
    public int physicalDamage = 0;
    // fire, lightning, etc.

    [Header("Final Damage")]
    private int finalDamage = 0; // Damage a character takes after all calcs are made

    // BUILD UPS
    // Build up effect amounts

    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false; // Force different damage animation (optional)
    public string damageAnimation;

    [Header("Poise")]
    public int poiseDamage = 0;
    public bool poiseIsBroken = false;

    [Header("Sound Effects")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamageSFX; // Not needed rn

    [Header("Direction Damage Taken From")]
    public float angleHitFrom; // Used to determine what damage animation to play
    public Vector3 contactPoint; // Point on collider damage is happening


    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        if (character.isDead)
            return;

        // Check for invulnerability

        CalculateDamage(character);
        // Check which direction damage came from
        // Play a damage animation
        // Check for buildups
        // PLay damage SFX and VFX

        // IF character is AI, check for new target if character causing damage is present

    }

    private void CalculateDamage(CharacterManager character)
    {
        if (characterCausingDamage != null)
        {
            // Check for damage modifiers on that character

        }

        // Check character for flat damage reduction

        // Check for armor absorptions???

        // Add all damage together, apply final damage
        finalDamage = physicalDamage;  // + elemental + etc.

        if (finalDamage <= 0)
        {
            finalDamage = 1;
        }

        Debug.Log("Damage taken: " + finalDamage);
        Debug.Log("Current health: " + character.currentHealth.Value);

        character.currentHealth.Value = character.currentHealth.Value - finalDamage;

        Debug.Log("Current health: " + character.currentHealth.Value);

        // Calculate Poise Damage

    }
}
