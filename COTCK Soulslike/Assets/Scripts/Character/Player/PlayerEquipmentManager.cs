using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    private PlayerManager player;

    public WeaponModelInstatiationSlot rightHandSlot;
    public WeaponModelInstatiationSlot leftHandSlot;

    public GameObject rightHandWeaponModel;
    public GameObject leftHandWeaponModel;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

        InitializeWeaponSlots();
    }

    protected override void Start()
    {
        base.Start();

        //LoadWeaponsOnBothHands();
        LoadRightWeapon();
    }

    private void InitializeWeaponSlots()
    {
        WeaponModelInstatiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstatiationSlot>();

        foreach (var weaponSlot in weaponSlots)
        {
            if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
            {
                rightHandSlot = weaponSlot;
            }
            else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
            {
                leftHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponsOnBothHands()
    {
        LoadLeftWeapon();
        LoadRightWeapon();
    }

    public void LoadRightWeapon()
    {
        if (player.playerInventoryManager.currentRightHandWeapon != null)
        {
            rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
        }
    }

    public void LoadLeftWeapon()
    {
        if (player.playerInventoryManager.currentLeftHandWeapon != null)
        {
            leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
            leftHandSlot.LoadWeapon(leftHandWeaponModel);
        }
    }
}
