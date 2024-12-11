using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEquipmentManager : CharacterEquipmentManager
{
    private AICharacterManager boss;

    public WeaponModelInstatiationSlot rightHandSlot;

    [SerializeField] private WeaponManager rightWeaponManager;


    public GameObject rightHandWeaponModel;

    protected override void Awake()
    {
        base.Awake();

        boss = GetComponent<AICharacterManager>();

        InitializeWeaponSlots();
    }

    protected override void Start()
    {
        base.Start();

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
        }
    }

    public void LoadRightWeapon()
    {
        if (boss.bossInventoryManager.currentRightHandWeapon != null)
        {
            rightHandWeaponModel = Instantiate(boss.bossInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
            rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
            rightWeaponManager.SetWeaponDamage(boss, boss.bossInventoryManager.currentRightHandWeapon);
        }
    }


    // DAMAGE COLLIDERS

    public void OpenDamageCollider()
    {
        Debug.Log("rightWeaponManager: " + rightWeaponManager == null);
        Debug.Log("manager collider: " + rightWeaponManager.meleeDamageCollider == null);
        rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
        rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
    }
}
