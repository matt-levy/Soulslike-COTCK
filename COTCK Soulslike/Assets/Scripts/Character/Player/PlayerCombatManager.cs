using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    private PlayerManager player;
    public WeaponItem currentWeaponBeingUsed;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void PerformWeaponBasedAction(WeaponItemAction action, WeaponItem weaponPerformingAction)
    {
        // Perform action
        action.AttemptToPerformAction(player, weaponPerformingAction);

    }

    public void DrainStaminaBasedOnAttack()
    {
        int staminaDeducted = 0;

        if (currentWeaponBeingUsed == null)
            return;

        switch (currentAttackType)
        {
            case AttackType.LightAttack01:
                staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaMultiplier;
                break;
            default:
                break;

        }

        player.currentStamina.Value -= staminaDeducted;

    }
}
