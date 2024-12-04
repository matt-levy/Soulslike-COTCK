using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }
    private void OnAnimatorMove() 
    {
        if (player.applyRootMotion) 
        {
            Vector3 velocity = player.animator.deltaPosition;
            player.characterController.Move(velocity);
            player.transform.rotation *= player.animator.deltaRotation;
        }
    }

    // ANIMATION EVENT CALLS
    public override void EnableCanDoCombo(){
        if (player.isUsingRightHand){
            player.playerCombatManager.canCombowithMainHandWeapon = true;
        }
        else{
            // ENABLE OFF HAND COMBO
        }
    }

    public override void DisableCanDoCombo(){
        player.playerCombatManager.canCombowithMainHandWeapon = false;
        // player.PlayerCombatManager.CanCombowithOffHandWeapon = false;
    }
}
