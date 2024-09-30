using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager Instance;

    [Header("Menu Objects")]
    [SerializeField] private GameObject titleScreenMainMenu;
    [SerializeField] private GameObject titleScreenLoadMenu;

    [Header("Buttons")]
    [SerializeField] private Button loadMenuReturnButton;
    [SerializeField] private Button mainMenuLoadGameButton;
    [SerializeField] private Button mainMenuNewGameButton;
    [SerializeField] private Button noCharacterSlotsOKButton;
    [SerializeField] private Button deleteCharacterSlotConfirmButton;

    [Header("Popups")]
    [SerializeField] private GameObject noCharacterSlotsPopup;
    [SerializeField] private GameObject deleteCharacterSlotPopup;

    [Header("Save Slots")]
    public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void StartNewGame()
    {
        WorldSaveGameManager.instance.AttemptToCreateNewGame();
        StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
    }

    public void OpenLoadGameMenu()
    {
        // Close main menu, open load menu
        titleScreenMainMenu.SetActive(false);
        titleScreenLoadMenu.SetActive(true);
        deleteCharacterSlotPopup.SetActive(false);

        loadMenuReturnButton.Select();
    }

    public void CloseLoadGameMenu()
    {
        titleScreenLoadMenu.SetActive(false);
        titleScreenMainMenu.SetActive(true);

        mainMenuLoadGameButton.Select();
    }

    public void DisplayNoFreeCharacterSlotsPopup()
    {   
        noCharacterSlotsPopup.SetActive(true);  
        noCharacterSlotsOKButton.Select();
    }

    public void CloseNoFreeCharacterSlotsPopup()
    {
        noCharacterSlotsPopup.SetActive(false);
        mainMenuNewGameButton.Select();
    }

    public void SelectCharacterSlot(CharacterSlot characterSlot)
    {
        currentSelectedSlot = characterSlot;
    }

    public void SelectNoSlot()
    {
        currentSelectedSlot = CharacterSlot.NO_SLOT;
    }

    public void AttemptToDeleteCharacterSlot()
    {
        if (currentSelectedSlot != CharacterSlot.NO_SLOT) 
        {
            deleteCharacterSlotPopup.SetActive(true);
            deleteCharacterSlotConfirmButton.Select();
        }
    }

    public void DeleteCharacterSlot()
    {
        WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);
        titleScreenLoadMenu.SetActive(false);
        titleScreenLoadMenu.SetActive(true);
        CloseDeleteCharacterPopup();
    }

    public void CloseDeleteCharacterPopup()
    {
        deleteCharacterSlotPopup.SetActive(false);
        loadMenuReturnButton.Select();
    }
}
