using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeDamageCollider : DamageCollider
{
    private AICharacterManager aiCharacter;

    protected override void Awake()
    {
        base.Awake();
        
        damageCollider = GetComponent<Collider>();
        aiCharacter = GetComponentInParent<AICharacterManager>();
    }
    protected override void DamageTarget(CharacterManager damageTarget)
    {
        // We dont want to damage the same target more than once in a single attack
        // So we add them to a list that checks before applying damage

        if (charactersDamaged.Contains(damageTarget))
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
        //     default:
        //         break;
        // }

        damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
    }
}
