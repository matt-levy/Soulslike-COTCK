using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    [HideInInspector] public NPCUIManager npcUIManager;
    [HideInInspector] public DialogueNode currentNode;
    private DialogueNode lastNode;
    private int currentLineIndex = 0;
    [SerializeField] private NPCManager npcManager;
    private bool isDialogueActive = false;

    private void Awake()
    {
        npcManager = GetComponent<NPCManager>();
    }

    public void ResumeDialogue(DialogueNode startNode)
    {
        if (lastNode != null)
        {
            StartDialogue(lastNode);
        }
        else
        {
            StartDialogue(startNode);
        }
    }

    public void StartDialogue(DialogueNode startNode)
    {
        isDialogueActive = true;
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
            npcUIManager.ShowContinueButton();
            string line = currentNode.dialogueText[currentLineIndex];
            npcUIManager.UpdateDialogueText(line);
        }
        else
        {
            npcUIManager.HideContinueButton();
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
        isDialogueActive = false;
        lastNode = currentNode;
        dialogueUI.SetActive(false);
        currentLineIndex = 0;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
