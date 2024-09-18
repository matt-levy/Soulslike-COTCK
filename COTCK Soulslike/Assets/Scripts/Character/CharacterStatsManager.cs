using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Stamina Regeneration")]
    private float staminaRegenTimer = 0;
    private float staminaTickTimer = 0;
    [SerializeField] private float staminaRegenDelay = 3;
    [SerializeField] private int staminaRegenAmount = 1;


    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public int CalculateTotalStaminaBasedOnLevel(int endurance)
    {
        float stamina;

        // Can make this calc much different in future
        // Perhaps a log function, tapers off over time
        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);

    }

    public virtual void RegenStamina() 
    {
        if (character.isSprinting || character.isPerformingAction)
            return;

        staminaRegenTimer += Time.deltaTime;

        if (staminaRegenTimer >= staminaRegenDelay)
        {
            if (character.currentStamina.Value < character.maxStamina)
            {
                staminaTickTimer += Time.deltaTime;

                if (staminaTickTimer >= 0.1)
                {
                    staminaTickTimer = 0;
                    character.currentStamina.Value += staminaRegenAmount;
                }
            }
        }
    }

    public virtual void ResetStaminaRegenTimer(int oldValue, int newValue)
    {
        // Only reset the regen timer if we lose stamina, not gain it.
        // If the new stamina amount is less than the old stamina amount,
        // then we are losing stamina
        if (newValue < oldValue)
        {
            staminaRegenTimer = 0;
        }
    }
}
