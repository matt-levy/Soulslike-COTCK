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

    public void ShootArrow()
    {
        Debug.Log("called");
        RangedProjectileItem projectileItem = player.playerInventoryManager.mainProjectile;

        if (projectileItem == null)
            return;


        Debug.Log("Projectile is not null");
    
        Transform projectileInstantiationLocation = player.playerCombatManager.lockOnTransform;
        GameObject liveProjectileGameObject = Instantiate(projectileItem.releaseProjectileModel, projectileInstantiationLocation);
        RangedProjectileDamageCollider liveProjectileDC = liveProjectileGameObject.GetComponent<RangedProjectileDamageCollider>();
        Rigidbody liveProjectileRB = liveProjectileDC.rb;

        liveProjectileDC.physicalDamage = 100;
        liveProjectileDC.characterShootingProjectile = player;

        if (player.isAiming.Value)
        {

        }
        else
        {
            // IF LOCKED ON TO TARGET
            if (player.playerCombatManager.currentTarget != null)
            {
                Quaternion arrowRotation = Quaternion.LookRotation(player.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - liveProjectileGameObject.transform.position);
                liveProjectileGameObject.transform.rotation = arrowRotation;
            }
            // Unlocked and Not aiming
            else
            {
                // Temporarily Make arrow face forward facing direction of the player
            }
        }

        // Get all character colliders and ignore self
        // Collider[] characterColliders = player.GetComponentsInChildren<Collider>();
        // List<Collider> collidersArrowWillIgnore = new List<Collider>();

        // foreach (var item in characterColliders)
        // {
        //     collidersArrowWillIgnore.Add(item);
        // }

        // foreach (Collider hitbox in collidersArrowWillIgnore)
        // {
        //     Physics.IgnoreCollision(liveProjectileDC, hitbox);
        // }

        liveProjectileRB.AddForce(liveProjectileGameObject.transform.forward * projectileItem.forwardVelocity);
        liveProjectileGameObject.transform.parent = null;

        // Reset to unloaded
        player.hasArrowLoaded = false;

    }
}
