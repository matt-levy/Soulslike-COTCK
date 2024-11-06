using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterEquipmentManager : CharacterEquipmentManager
{
    private AICharacterManager aiCharacter;

    public WeaponModelInstatiationSlot rightHandSlot;
    public WeaponModelInstatiationSlot leftHandSlot;

    [SerializeField] private WeaponManager leftWeaponManager;
    [SerializeField] private WeaponManager rightWeaponManager;


    public GameObject rightHandWeaponModel;
    public GameObject leftHandWeaponModel;

    protected override void Awake()
    {
        base.Awake();

        aiCharacter = GetComponent<AICharacterManager>();

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
        if (aiCharacter.aiCharacterInventoryManager.currentRightHandWeapon != null)
        {
            rightHandWeaponModel = Instantiate(aiCharacter.aiCharacterInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
            rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
            rightWeaponManager.SetWeaponDamage(aiCharacter, aiCharacter.aiCharacterInventoryManager.currentRightHandWeapon);
        }
    }

    public void LoadLeftWeapon()
    {
        if (aiCharacter.aiCharacterInventoryManager.currentLeftHandWeapon != null)
        {
            leftHandWeaponModel = Instantiate(aiCharacter.aiCharacterInventoryManager.currentLeftHandWeapon.weaponModel);
            leftHandSlot.LoadWeapon(leftHandWeaponModel);
            leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
            leftWeaponManager.SetWeaponDamage(aiCharacter, aiCharacter.aiCharacterInventoryManager.currentLeftHandWeapon);
        }
    }

    // DAMAGE COLLIDERS

    public void OpenDamageCollider()
    {
        if (aiCharacter.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
        } 
        else if (aiCharacter.isUsingLeftHand)
        {
            leftWeaponManager.meleeDamageCollider.EnableDamageCollider();
        }

        // Play whoosh sfx
    }

    public void CloseDamageCollider()
    {
        if (aiCharacter.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
        } 
        else if (aiCharacter.isUsingLeftHand)
        {
            leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
        }

        // Play whoosh sfx
    }
}
