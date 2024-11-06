using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPCUIManager : MonoBehaviour
{
    public static NPCUIManager instance;

    public TMP_Text npcDialogueText;
    public TMP_Text npcNameText;
    public GameObject choicePanel;
    public Button choiceButtonPrefab;
    public Button continueButton;

    private int currentChoiceIndex = 0;
    private List<Button> choiceButtons = new List<Button>();

    private bool canSelectChoice = false;
    private float debounceTime = 0.2f;
    private float timeSinceLastInput = 0f;



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
        if (gameObject.activeInHierarchy)
        {
            // Update the timer
            timeSinceLastInput += Time.deltaTime;

            // If the continue button is active and the player presses E
            if (continueButton.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                continueButton.onClick.Invoke();
            }

            // If the choice panel is active, allow cycling through choices
            if (choicePanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SelectPreviousChoice();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SelectNextChoice();
                }
                else if (Input.GetKeyDown(KeyCode.E) && canSelectChoice)
                {
                    choiceButtons[currentChoiceIndex].onClick.Invoke();
                    canSelectChoice = false; // Reset the flag once a choice is made
                }

                // Enable choice selection after the debounce time has passed
                if (timeSinceLastInput >= debounceTime)
                {
                    canSelectChoice = true;
                }
            }
        }
    }


    public void SetUpDialogue(NPCDialogueManager manager, string npcName)
    {
        npcDialogueManager = manager;
        npcNameText.text = npcName;
        ShowContinueButton();
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
            choiceButtons.Add(choiceButton);
        }

        // Set the first choice as selected initially
        currentChoiceIndex = 0;
        UpdateChoiceSelection();

        // Reset the timer and prevent immediate selection
        timeSinceLastInput = 0f;
        canSelectChoice = false;
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
        choiceButtons.Clear();
        currentChoiceIndex = 0;
    }

    public void OnContinueButtonClicked()
    {
        npcDialogueManager.AdvanceDialogue();
    }

    public void ShowContinueButton()
    {
        continueButton.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
    }

    public void HideContinueButton()
    {
        continueButton.gameObject.SetActive(false);
    }

    private void SelectPreviousChoice()
    {
        // Decrease the index and wrap around if it's less than 0
        currentChoiceIndex--;
        if (currentChoiceIndex < 0)
        {
            currentChoiceIndex = choiceButtons.Count - 1;
        }
        UpdateChoiceSelection();
    }

    private void SelectNextChoice()
    {
        // Increase the index and wrap around if it exceeds the number of choices
        currentChoiceIndex++;
        if (currentChoiceIndex >= choiceButtons.Count)
        {
            currentChoiceIndex = 0;
        }
        UpdateChoiceSelection();
    }

    private void UpdateChoiceSelection()
    {
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            // Highlight the current choice and reset the others
            if (i == currentChoiceIndex)
            {
                choiceButtons[i].Select(); // This highlights the button
            }
        }
    }
}
