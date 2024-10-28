using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAllyManager : CharacterManager
{
    public enum AllyState { Idle, FollowPlayer, Attack, FollowEnemy }
    public AllyState currentState;

    public Transform playerTransform;
    [HideInInspector] public PlantAllyLocomotionManager locomotionManager;
    [HideInInspector] public PlantAllyAnimationManager animationManager;
    [HideInInspector] public MeleeWeaponDamageCollider damageCollider;

    [Header("Settings")]
    public float followRange = 5f;
    public float attackRange = 1f;
    public float enemyDetectionRange = 10f;
    public float maxDistanceFromPlayer = 15f;

    private Transform targetEnemy;

    private void Start()
    {
        currentState = AllyState.Idle;
        playerTransform = GameObject.FindWithTag("Player").transform;
        locomotionManager = GetComponent<PlantAllyLocomotionManager>();
        animationManager = GetComponent<PlantAllyAnimationManager>();
        damageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case AllyState.Idle:
                HandleIdleState();
                DetectEnemies();
                break;

            case AllyState.FollowPlayer:
                FollowPlayer();

                // Only detect enemies if near enough to player
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
                if (distanceToPlayer < maxDistanceFromPlayer)
                {
                    DetectEnemies();
                }
                break;

            case AllyState.FollowEnemy:
                FollowEnemy();
                break;

            case AllyState.Attack:
                if (targetEnemy != null)
                {
                    AttackTarget();
                }
                break;
        }
    }

    private void HandleIdleState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > followRange)
        {
            currentState = AllyState.FollowPlayer;
        }
        else
        {
            animationManager.PlayIdleAnimation();
        }
    }

    private void DetectEnemies()
    {
        // Get all enemies in range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, enemyDetectionRange, LayerMask.GetMask("Enemy"));

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // Get the closest enemy
        foreach (Collider enemy in enemiesInRange)
        {
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance && IsInLineOfSight(enemy.transform))
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            currentState = AllyState.FollowEnemy;
        }
    }

    private bool IsInLineOfSight(Transform target)
    {
        // Cast a ray to check if the enemy is in LOS
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * enemyDetectionRange, Color.red);
        if (Physics.Raycast(transform.position, direction, out hit, enemyDetectionRange))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Follow the player if they are out of range
        if (distanceToPlayer > followRange)
        {
            locomotionManager.MoveTowards(playerTransform.position);
            animationManager.PlayWalkAnimation();
        }
        // Go Idle if near the player
        else
        {
            locomotionManager.StopMovement();
            animationManager.PlayIdleAnimation();
            currentState = AllyState.Idle;
        }
    }

    private void FollowEnemy()
    {
        if (targetEnemy == null)
        {
            currentState = AllyState.Idle;
            return;
        }

        float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Return to the player if they are too far away
        if (distanceToPlayer > maxDistanceFromPlayer)
        {
            targetEnemy = null;
            currentState = AllyState.FollowPlayer;
        }
        // Attack enemy if close enough
        else if (distanceToEnemy <= attackRange)
        {
            currentState = AllyState.Attack;
        }
        // Follow the closest enemy
        else
        {
            locomotionManager.MoveTowards(targetEnemy.position);
            animationManager.PlayWalkAnimation();
        }
    }

    public void AttackTarget()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);

        if (distanceToEnemy <= attackRange)
        {
            locomotionManager.StopMovement();
            locomotionManager.LookAtTarget(targetEnemy.position);
            animationManager.PlayAttackAnimation();
            damageCollider.EnableDamageCollider();
            StartCoroutine(DisableDamageColliderAfterAttack());
        }
        else
        {
            currentState = AllyState.FollowEnemy;
        }
    }

    private IEnumerator DisableDamageColliderAfterAttack()
    {
        yield return new WaitForSeconds(animationManager.GetAttackAnimationDuration());
        damageCollider.DisableDamageCollider();

        DetectEnemies();
        // If enemy is still near keep following
        if (targetEnemy != null)
        {
            currentState = AllyState.FollowEnemy;
            animationManager.PlayWalkAnimation();
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > followRange)
            {
                currentState = AllyState.FollowPlayer;
            }
            else
            {
                currentState = AllyState.Idle;
            }
        }
    }
}
