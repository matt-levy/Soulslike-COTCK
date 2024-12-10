using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedProjectileDamageCollider : DamageCollider
{
    public CharacterManager characterShootingProjectile;
    public Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            //hasCollided = true;

            CharacterManager potentialTarget = collision.transform.gameObject.GetComponent<CharacterManager>();

            // perform arrow penetration

            if (characterShootingProjectile == null)
                return;

            if (potentialTarget == null)
                return;

            if (WorldUtilityManager.instance.CanIDamageThisTarget(characterShootingProjectile.characterGroup, potentialTarget.characterGroup))
            {
                DamageTarget(potentialTarget);
            }

            Destroy(gameObject);
        }

    }
}
