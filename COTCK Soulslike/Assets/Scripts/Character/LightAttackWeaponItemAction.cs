using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]
public class LightAttackWeaponItemAction : WeaponItemAction
{
    [SerializeField] private string light_attack_01 = "Main_Light_Attack_01"; // Main means main hand
    [SerializeField] private string light_attack_02 = "Main_Light_Attack_02"; // Main means main hand

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        // Check for stops

        // if (playerPerformingAction.currentStamina.Value <= 0)
        //     return;


        PerformLightAttack(playerPerformingAction, weaponPerformingAction);
    }

    private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // IF WE ARE CURRENTLY ATTACKING, AND WE CAN COMBO, PERFORM THE COMBO ATTACK
        if (playerPerformingAction.playerCombatManager.canCombowithMainHandWeapon && playerPerformingAction.isPerformingAction)
        {
            playerPerformingAction.playerCombatManager.canCombowithMainHandWeapon = false;

            //  PERFORM AN ATTACK BASED ON THE PREVIOUS ATTACK WE JUST PLAYED
            if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == light_attack_01)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack02, light_attack_02, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_attack_01, true);
            }
        }
        // OTHERWISE JUST PERFORM A REGLAR ATTACK, IF WE ARE NOT ALREADY ATTACKING
        else if (!playerPerformingAction.isPerformingAction)
        {
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_attack_01, true);
        }
        
        /*if (playerPerformingAction.isUsingRightHand)
        {
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_attack_01, true);
        }

        if (playerPerformingAction.isUsingLeftHand)
        {

        } */
    } 
}
