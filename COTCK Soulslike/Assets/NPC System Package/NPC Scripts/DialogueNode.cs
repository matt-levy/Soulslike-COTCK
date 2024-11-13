using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(1, 10)]
    public List<string> dialogueText;
    public List<AudioClip> dialogueAudio;
    public List<DialogueChoice> choices;
    public bool isEndNode;

    public bool HasChoices()
    {
        return choices != null && choices.Count > 0;
    }
}

// DialogueChoice class for branching options
[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueNode nextNode;
    public string condition;
}
