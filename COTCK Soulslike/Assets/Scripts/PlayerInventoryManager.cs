using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryManager : MonoBehaviour
{
    // Weapon slots
    public WeaponSlot mainHandSlot;
    public WeaponSlot offHandSlot;

    // Available but not equipped weapons
    public List<Weapon> weaponInventory = new List<Weapon>();

    // Color Slots
    public ColorSlot[] colorSlots;

    // Consumables
    public Hashtable consumables = new Hashtable();

    // Arrows (for the crossbow)
    public int arrowCount;

    public int totalColorSlots = 1;

    private void Start()
    {
        mainHandSlot = new WeaponSlot();
        offHandSlot = new WeaponSlot();
        colorSlots = new ColorSlot[totalColorSlots];

        for (int i = 0; i < colorSlots.Length; i++)
        {
            colorSlots[i] = new ColorSlot();
        }

        arrowCount = 0;
    }

    // Equip a weapon to a specific hand/slot
    public void EquipWeapon(Weapon weapon, bool isMainHand)
    {
        if (isMainHand)
        {
            mainHandSlot.EquipWeapon(weapon);
            Debug.Log("Equipped " + weapon.Name + " to main hand");
        }
        else
        {
            offHandSlot.EquipWeapon(weapon);
            Debug.Log("Equipped " + weapon.Name + " to off hand");
        }
        RemoveWeaponFromInventory(weapon);
    }

    // Swap main and off hand weapons
    public void SwapWeapons()
    {
        Weapon temp = mainHandSlot.equippedWeapon;
        mainHandSlot.EquipWeapon(offHandSlot.equippedWeapon);
        offHandSlot.EquipWeapon(temp);
        Debug.Log("Swapped weapons");
    }

    public void EquipColor(ColorPassive color, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < colorSlots.Length)
        {
            colorSlots[slotIndex].EquipColor(color);
        }
    }

    public void StoreConsumable(Consumable consumable, string category)
    {
        if (consumables.ContainsKey(category))
        {
            List<Consumable> consumableList = (List<Consumable>)consumables[category];
            consumableList.Add(consumable);
        }
        else
        {
            consumables[category] = new List<Consumable>();
            List<Consumable> consumableList = (List<Consumable>)consumables[category];
            consumableList.Add(consumable);

        }
    }

    public void UseConsumable(string category)
    {
        if (consumables.ContainsKey(category))
        {
            List<Consumable> consumableList = (List<Consumable>)consumables[category];

            if (consumableList.Count > 0)
            {
                // Use the first consumable in the list
                consumableList[0].Use();
                consumableList.Remove(consumableList[0]);
            }
        }
    }

    public void StoreArrows(int amount)
    {
        arrowCount += amount;
        Debug.Log("Picked up arrows. Total now: " + arrowCount);
    }

    public void ShootArrow()
    {
        arrowCount -= 1;
    }

    public void AddWeaponToInventory(Weapon weapon)
    {
        weaponInventory.Add(weapon);
    }

    public void RemoveWeaponFromInventory(Weapon weapon)
    {
        weaponInventory.Remove(weapon);
    }
}
