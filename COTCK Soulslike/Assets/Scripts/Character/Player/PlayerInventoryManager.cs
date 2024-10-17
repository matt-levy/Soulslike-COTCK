using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public WeaponItem currentRightHandWeapon;
    public WeaponItem currentLeftHandWeapon;

    public ColorPassive currentColorPassive;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateColorPassive();
        }
    }

    public void ActivateColorPassive()
    {
        if (currentColorPassive != null)
        {
            if (currentColorPassive is GreenColorPassive greenColorPassive)
            {
                greenColorPassive.SummonPlantAlly(gameObject.transform);
            }
        }
    }
}
