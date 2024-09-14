using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot : MonoBehaviour
{
    public Weapon equippedWeapon;

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
        Debug.Log("Equipped Weapon: " + weapon.Name);
    }
}
