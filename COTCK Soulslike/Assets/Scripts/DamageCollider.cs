using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Damage Types")]
    public int physicalDamage = 0;

    [Header("Contact Point")]
    protected Vector3 contactPoint;

    [Header("Characters Damaged")]
    protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager damageTarget = other.GetComponent<CharacterManager>();

        if (damageTarget != null)
        {
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            
            // Check if we can damage this target based on friendly fire

            // check if target is blocking

            // check if target is invulnerable

            // damage!

            DamageTarget(damageTarget);
        }
    }

    protected virtual void DamageTarget(CharacterManager damageTarget)
    {
        // We dont want to damage the same target more than once in a single attack
        // So we add them to a list that checks before applying damage

        if (charactersDamaged.Contains(damageTarget))
            return;

        charactersDamaged.Add(damageTarget);

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;

        damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
    }
}
