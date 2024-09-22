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
    public Dictionary<string, int> arrowsDict = new Dictionary<string, int>();

    public ColorSlot colorSlot;
    public List<ColorPassive> colorInventory = new List<ColorPassive>();


    private void Start()
    {
        mainHandSlot = new WeaponSlot();
        offHandSlot = new WeaponSlot();
        colorSlot = new ColorSlot();
    }

    // Equip a weapon to a specific hand/slot
    public void EquipWeapon(Weapon weapon, bool isMainHand)
    {
        if (isMainHand)
        {
            if (mainHandSlot.equippedWeapon != null)
            {
                weaponInventory.Add(mainHandSlot.equippedWeapon);
            }
            mainHandSlot.EquipWeapon(weapon);
            Debug.Log("Equipped " + weapon.Name + " to main hand");
        }
        else
        {
            if (offHandSlot.equippedWeapon != null)
            {
                weaponInventory.Add(offHandSlot.equippedWeapon);
            }
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

    public void EquipColor(ColorPassive color)
    {
        if (colorSlot.equippedColor != null)
        {
            colorInventory.Add(colorSlot.equippedColor);
        }
        colorSlot.EquipColor(color);
        RemoveColorFromInventory(color);
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

    public void StoreArrows(int amount, string category)
    {
        if (arrowsDict.ContainsKey(category))
        {
            arrowsDict[category] += amount;
        }
        else
        {
            arrowsDict[category] = amount;
        }
    }

    public void ShootArrow(string category)
    {
        arrowsDict[category] -= 1;
    }

    public void AddWeaponToInventory(Weapon weapon)
    {
        weaponInventory.Add(weapon);
    }

    public void RemoveWeaponFromInventory(Weapon weapon)
    {
        weaponInventory.Remove(weapon);
    }

    public void AddColorToInventory(ColorPassive color)
    {
        colorInventory.Add(color);
    }

    public void RemoveColorFromInventory(ColorPassive color)
    {
        colorInventory.Remove(color);
    }
}
