using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCUIManager : MonoBehaviour
{
    public static NPCUIManager instance;

    public TMP_Text npcDialogueText;
    public TMP_Text npcNameText;
    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    private NPCDialogueManager npcDialogueManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            OnContinueButtonClicked();
        }
    }

    public void SetUpDialogue(NPCDialogueManager manager, string npcName)
    {
        npcDialogueManager = manager;
        npcNameText.text = npcName;
        ClearChoices();
    }

    public void UpdateDialogueText(string text)
    {
        npcDialogueText.text = text;
    }

    public void DisplayChoices(List<DialogueChoice> choices)
    {
        ClearChoices();
        choicePanel.SetActive(true);

        for (int i = 0; i < choices.Count; i++)
        {
            DialogueChoice choice = choices[i];
            Button choiceButton = Instantiate(choiceButtonPrefab, choicePanel.transform);
            choiceButton.GetComponentInChildren<TMP_Text>().text = choice.choiceText;
            int choiceIndex = i;
            choiceButton.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        npcDialogueManager.MakeChoice(choiceIndex);
        ClearChoices();
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }
        choicePanel.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        npcDialogueManager.AdvanceDialogue();
    }
}
