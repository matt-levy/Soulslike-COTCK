using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [Header("UI")]
    public bool hasFloatingHPBar = true;
    public UI_Character_HP_Bar characterHPBar;

    public void OnHPChanged(int oldValue, int newValue)
    {
        characterHPBar.SetStat(newValue);
    }
}
