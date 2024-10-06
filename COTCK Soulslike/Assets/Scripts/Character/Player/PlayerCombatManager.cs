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
}
