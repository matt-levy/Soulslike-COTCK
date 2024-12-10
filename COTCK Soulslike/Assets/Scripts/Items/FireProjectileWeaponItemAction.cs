using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Fire Projectile Action")]
public class FireProjectileWeaponItemAction : WeaponItemAction
{   
    [SerializeField] ProjectileSlot projectileSlot;
    [SerializeField] GameObject arrowModel;

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {

        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        Debug.Log("called action");

        if (playerPerformingAction.currentStamina.Value <= 0)
             return;

        RangedProjectileItem projectileItem;

        // if adding multiple projectiles, use a switch statement
        projectileItem = playerPerformingAction.playerInventoryManager.mainProjectile;

        if (projectileItem == null)
            return;

        Debug.Log("projectile does not equal null");

        if (!playerPerformingAction.hasArrowLoaded)
        {
            GameObject arrow = Instantiate(arrowModel, playerPerformingAction.playerEquipmentManager.leftHandSlot.transform);
            playerPerformingAction.playerAnimatorManager.PlayTargetActionAnimation("Reload_01", true);
            playerPerformingAction.hasArrowLoaded = true;
        }
        else 
        {
            playerPerformingAction.playerCombatManager.ShootArrow();
        }
        
    }
}
