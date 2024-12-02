using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamageCollider : DamageCollider
{
    [Header("Attacking Character")]
    public CharacterManager characterCausingDamage;

    [Header("Weapon Attack Modifiers")]
    public int light_attack_01_modifier;
    public int light_attack_02_modifier;

    protected override void Awake()
    {
        base.Awake();

        damageCollider.enabled = false;

    }

    protected override void OnTriggerEnter(Collider other)
    {
        CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

        if (damageTarget != null)
        {
            // we don't want to damage ourselves
            if (damageTarget == characterCausingDamage)
                return;
            
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            
            // Check if we can damage this target based on friendly fire

            // check if target is blocking

            // check if target is invulnerable


            DamageTarget(damageTarget);
        }
    }

    protected override void DamageTarget(CharacterManager damageTarget)
    {
        // We dont want to damage the same target more than once in a single attack
        // So we add them to a list that checks before applying damage

        if (charactersDamaged.Contains(damageTarget))
            return;

        // Don't damage target if they are invincible
        if (damageTarget.isInvincible)
            return;

        charactersDamaged.Add(damageTarget);

        //Debug.Log(charactersDamaged[0]);

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;

        // switch (characterCausingDamage.characterCombatManager.currentAttackType)
        // {
        //     case AttackType.LightAttack01:
        //         ApplyAttackDamageModifiers(light_attack_01_modifier, damageEffect);
        //         break;
        //     case AttackType.LightAttack02:
        //         ApplyAttackDamageModifiers(light_attack_02_modifier, damageEffect);
        //         break;   
        //     default:
        //         break;
        // }

        damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
    }

    // Change modifiers to floats in the future, cast to int
    private void ApplyAttackDamageModifiers(int modifier, TakeDamageEffect damage)
    {
        damage.physicalDamage *= modifier;
        
        // if attack is fully charged, multiply by another modifier after normal mods

    }

}
