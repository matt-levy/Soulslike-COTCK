using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : CharacterManager
{
    public string npcName;
    [HideInInspector] public NPCInteractionManager interactionManager;
    [HideInInspector] public NPCDialogueManager dialogueManager;
    [HideInInspector] public NPCAnimationManager animationManager;
    [HideInInspector] public NPCMovementManager movementManager;

    protected override void Awake()
    {
        base.Awake();

        interactionManager = GetComponent<NPCInteractionManager>();
        dialogueManager = GetComponent<NPCDialogueManager>();
        animationManager = GetComponent<NPCAnimationManager>();
        movementManager = GetComponent<NPCMovementManager>();

    }
}
