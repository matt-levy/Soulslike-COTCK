using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    // variable to scale size of bar based on stat level
    // Secondary bar behind main bar for polish effect

    protected virtual void Awake() 
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetStat(int newValue)
    {
        slider.value = newValue;
    }

    public virtual void SetMaxStat(int maxValue)
    {
        slider.maxValue = maxValue;
    }

}
