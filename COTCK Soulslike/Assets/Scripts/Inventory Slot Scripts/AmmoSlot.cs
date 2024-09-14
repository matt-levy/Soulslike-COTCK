using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSlot : MonoBehaviour
{
    public Ammo equippedAmmo;

    public void AddAmmunition(Ammunition ammo)
    {
        equippedAmmo = ammo;
        Debug.Log("Added Ammunition: " + ammo.ammoType);
    }
}
