using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AICharacterManager : CharacterManager
{
    [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
    [HideInInspector] public AICharacterLocomotionManager aICharacterLocomotionManager;

    [Header("Navmesh Agent")]
    public NavMeshAgent navMeshAgent;

    [Header("Current State")]
    [SerializeField] private AIState currentState;

    [Header("States")]
    public IdleState idle;
    public PursueTargetState pursueTarget;
    public CombatStanceState combatStance;
    public AttackState attack;

    protected override void Awake()
    {
        base.Awake();

        aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        aICharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();

        // Use copy of scriptable object so original is not modified
        idle = Instantiate(idle);
        pursueTarget = Instantiate(pursueTarget);

        currentState = idle;
    }

    protected override void Update()
    {
        base.Update();

        aiCharacterCombatManager.HandleActionRecovery(this);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ProcessStateMachine();
    }

    private void ProcessStateMachine()
    {
        AIState nextState = null;

        if (currentState != null)
        {
            nextState = currentState.Tick(this);
        }

        if (nextState != null)
        {
            currentState = nextState;
        }

        navMeshAgent.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (aiCharacterCombatManager.currentTarget != null)
        {
            aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombatManager.currentTarget.transform.position);
        }

        if (navMeshAgent.enabled)
        {
            Vector3 agentDestination = navMeshAgent.destination;
            float remainingDistance = Vector3.Distance(agentDestination, transform.position);
            Debug.Log(navMeshAgent.destination);

            if (remainingDistance > navMeshAgent.stoppingDistance)
            {
                isMoving.Value = true;
            }
            else
            {
                isMoving.Value = false;
            }
        }
        else
        {
            isMoving.Value = false;
        }
    }
}
