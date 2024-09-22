using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSlot : MonoBehaviour
{
    public ColorPassive equippedColor;

    public void EquipColor(ColorPassive color)
    {
        equippedColor = color;
        Debug.Log("Equipped Color");
    }
}
