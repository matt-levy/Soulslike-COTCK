using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnarmedCombatManager : AICharacterCombatManager
{
    [Header("Damage Colliders")]
    [SerializeField] AIMeleeDamageCollider rightHandDC;
    [SerializeField] AIMeleeDamageCollider leftHandDC;

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] int attack01DamageModifier = 1;

    public void SetAttack01Damage()
    {
        rightHandDC.physicalDamage = baseDamage * attack01DamageModifier;
        leftHandDC.physicalDamage = baseDamage * attack01DamageModifier;
    }

    public void OpenRightHandDamageCollider()
    {
        rightHandDC.EnableDamageCollider();
    }

    public void CloseRightHandDamageCollider()
    {
        rightHandDC.DisableDamageCollider();
    }

    public void OpenLeftHandDamageCollider()
    {
        leftHandDC.EnableDamageCollider();
    }

    public void CloseLeftHandDamageCollider()
    {
        leftHandDC.DisableDamageCollider();
    }
}
