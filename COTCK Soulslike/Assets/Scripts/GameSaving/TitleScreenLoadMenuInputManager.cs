using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenLoadMenuInputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Title Screen Inputs")]
    [SerializeField] private bool deleteCharacterSlot = false;

    private void Update()
    {
        if (deleteCharacterSlot)
        {
            TitleScreenManager.Instance.AttemptToDeleteCharacterSlot();
            deleteCharacterSlot = false;
        }
    }

    // private void OnEnable()
    // {
    //     if (playerControls == null)
    //     {
    //         playerControls = new PlayerControls();
    //         playerControls.UI.X.performed += i => deleteCharacterSlot = true;
    //     }

    //     playerControls.Enable();
    // }

    // private void OnDisable()
    // {
    //     playerControls.Disable();
    // }
}
