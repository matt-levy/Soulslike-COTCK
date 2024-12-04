using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public MeleeWeaponDamageCollider meleeDamageCollider;

    private void Awake()
    {
        meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
    }

    public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
    {
        if (meleeDamageCollider == null)
            return;
            
        meleeDamageCollider.physicalDamage = weapon.physicalDamage;
        // Add other damage types as well
        // Add scaling damage from stats

        meleeDamageCollider.light_attack_01_modifier = weapon.lightAttack01DamageMultiplier;
        meleeDamageCollider.light_attack_02_modifier = weapon.lightAttack02DamageMultiplier;


        
    }
}
