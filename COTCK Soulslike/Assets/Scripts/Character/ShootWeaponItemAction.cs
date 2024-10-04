using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Shoot Attack Action")]
public class ShootWeaponItemAction : WeaponItemAction
{
    // TODO Play the correct animation
    [SerializeField] private string shoot_attack_01 = "Main_Light_Attack_01"; // Main means main hand
    public GameObject arrowPrefab;
    private Transform arrowSpawnPoint;
    public float arrowForce = 20f;

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        // Check for stops

        if (playerPerformingAction.currentStamina.Value <= 0)
            return;


        PerformShootAttack(playerPerformingAction, weaponPerformingAction);
    }

    private void PerformShootAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        if (playerPerformingAction.isUsingRightHand)
        {
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(shoot_attack_01, true);
            arrowSpawnPoint = weaponPerformingAction.weaponModel.transform.Find("Weapon Pivot/Weapon Model/Arrow Spawn Point");
            ShootArrow(playerPerformingAction);
        }

        if (playerPerformingAction.isUsingLeftHand)
        {

        }
    }

    private void ShootArrow(PlayerManager playerPerformingAction)
    {
        // Instantiate the arrow
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // Apply force in the direction the player is aiming
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootDirection = playerPerformingAction.transform.forward;
            rb.AddForce(shootDirection * arrowForce, ForceMode.Impulse);
        }
    }
}
