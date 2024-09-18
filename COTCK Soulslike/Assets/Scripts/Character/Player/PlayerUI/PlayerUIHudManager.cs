using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_StatBar staminaBar;

    public void SetNewStaminaValue(int oldValue, int newValue)
    {
        staminaBar.SetStat(newValue);
    }

    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }
}
