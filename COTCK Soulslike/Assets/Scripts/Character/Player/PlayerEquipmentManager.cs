using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    private PlayerManager player;

    public WeaponModelInstatiationSlot rightHandSlot;
    public WeaponModelInstatiationSlot leftHandSlot;

    [SerializeField] private WeaponManager leftWeaponManager;
    [SerializeField] private WeaponManager rightWeaponManager;


    public GameObject rightHandWeaponModel;
    public GameObject leftHandWeaponModel;

    // Assumes sword is ALWAYS the starting weapon
    // Sword must begin in right hand, crossbow must be in left
    public bool isMelee = true;

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
            rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
        }
    }

    // The way this code works, left hand weapon is never loaded and is treaded as an offhand
    // This function switches the left and right hand weapons 
    // Right weapon is active weapon
    // IF LEFT HANDED MODE IS ADDED: update to accommodate 
    public void SwitchWeapon()
    {
        player.playerInventoryManager.SwitchInventoryWeapons();
        Destroy(rightHandWeaponModel);
        LoadRightWeapon();
    }

    public void LoadLeftWeapon()
    {
        if (player.playerInventoryManager.currentLeftHandWeapon != null)
        {
            leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
            leftHandSlot.LoadWeapon(leftHandWeaponModel);
            leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }
    }

    // DAMAGE COLLIDERS

    public void OpenDamageCollider()
    {
        if (player.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
        } 
        else if (player.isUsingLeftHand)
        {
            leftWeaponManager.meleeDamageCollider.EnableDamageCollider();
        }

        // Play whoosh sfx
    }

    public void CloseDamageCollider()
    {
        if (player.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
        } 
        else if (player.isUsingLeftHand)
        {
            leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
        }

        // Play whoosh sfx
    }
}
