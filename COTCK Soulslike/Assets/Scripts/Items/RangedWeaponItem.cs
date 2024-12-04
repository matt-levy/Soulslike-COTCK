using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons/Ranged Weapon")]
public class RangedWeaponItem : WeaponItem
{
    [Header("Ranged SFX")]
    public AudioClip[] drawSounds;
    public AudioClip[] releaseSounds;
}
