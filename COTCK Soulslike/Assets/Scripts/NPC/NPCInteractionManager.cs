using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionManager : MonoBehaviour
{
    [HideInInspector] public bool isPlayerInRange = false;
    private NPCManager npcManager;
    public DialogueNode startDialogueNode;

    private void Start()
    {
        npcManager = GetComponent<NPCManager>();
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            npcManager.dialogueManager.ResumeDialogue(startDialogueNode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            npcManager.dialogueManager.EndDialogue();
        }
    }
}
