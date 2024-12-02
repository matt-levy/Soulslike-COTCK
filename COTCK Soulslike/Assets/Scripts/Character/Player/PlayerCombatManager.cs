using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    private PlayerManager player;
    public WeaponItem currentWeaponBeingUsed;

    [Header("Flags")]
    public bool canCombowithMainHandWeapon = false;
    // public bool canCombowithOffHandWeapon = false;

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

    public override void SetTarget(CharacterManager newTarget)
    {
        base.SetTarget(newTarget);

        PlayerCamera.instance.SetLockCameraHeight();
    }
}
