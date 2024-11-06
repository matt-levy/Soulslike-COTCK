using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack")]
public class AICharacterAttackAction : ScriptableObject
{
    [Header("Attack Animation")]
    [SerializeField] private string attackAnimation;

    [Header("Combo Action")]
    public AICharacterAttackAction comboAction; // The combo action of this attack action

    [Header("Action Values")]
    public int attackWeight = 50;
    // Attack type
    // attack can be repeated
    public float actionRecoveryTime = 1.5f; // Time before character can make another attack action
    public float minAttackAngle = -35f;
    public float maxAttackAngle = 35f;
    public float minAttackDist = 0f;
    public float maxAttackDist = 2f;

    public void AttemptToPerformAction(AICharacterManager aiCharacter)
    {
        aiCharacter.characterAnimatorManager.PlayTargetActionAnimation(attackAnimation, true);
    }
}
