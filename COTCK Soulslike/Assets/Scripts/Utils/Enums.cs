using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    
}

public enum CharacterSlot
{   
    CharacterSlot_01,
    CharacterSlot_02,
    CharacterSlot_03,
    CharacterSlot_04,
    CharacterSlot_05,
    CharacterSlot_06,
    CharacterSlot_07,
    CharacterSlot_08,
    CharacterSlot_09,
    CharacterSlot_10,
    NO_SLOT
}

public enum CharacterGroup
{
    Team01,
    Team02
}
public enum WeaponModelSlot
{
    RightHand,
    LeftHand
    // Back slot for stowed weapons
}


public enum AttackType
{
    LightAttack01,
    LightAttack02
}

public enum ProjectileClass 
{
    Arrow,
    Bolt
}

// For if we want to add multiple projectile options
// Currently not necessary
public enum ProjectileSlot
{
    Main,
    Secondary
}