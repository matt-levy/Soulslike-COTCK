using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    [HideInInspector] public NPCUIManager npcUIManager;
    [HideInInspector] public DialogueNode currentNode;
    private int currentLineIndex = 0;
    private NPCManager npcManager;

    private void Start()
    {
        npcManager = GetComponent<NPCManager>();
    }

    public void StartDialogue(DialogueNode startNode)
    {
        currentNode = startNode;
        if (npcUIManager == null)
        {
            npcUIManager = dialogueUI.GetComponent<NPCUIManager>();
        }
        if (npcUIManager != null && currentNode != null)
        {
            dialogueUI.SetActive(true);
            npcUIManager.SetUpDialogue(this, npcManager.npcName);
            currentLineIndex = 0;
            ShowDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (currentLineIndex < currentNode.dialogueText.Count)
        {
            string line = currentNode.dialogueText[currentLineIndex];
            npcUIManager.UpdateDialogueText(line);
        }
        else
        {
            ShowChoices();
        }
    }

    public void AdvanceDialogue()
    {
        currentLineIndex++;
        ShowDialogue();
    }

    private void ShowChoices()
    {
        if (currentNode.HasChoices())
        {
            npcUIManager.DisplayChoices(currentNode.choices);
        }
        else
        {
            EndDialogue();
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        DialogueChoice chosen = currentNode.choices[choiceIndex];
        if (chosen.nextNode != null)
        {
            StartDialogue(chosen.nextNode);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        currentLineIndex = 0;
    }
}
