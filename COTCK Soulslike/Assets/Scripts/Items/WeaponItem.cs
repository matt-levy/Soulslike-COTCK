using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    // Animator Controller Override (Change attack animations based on weapon you are currently using)

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Requirements")]
    public int strReq = 0;
    public int dexReq = 0;

    [Header("Weapon Base Damage")]
    public int physicalDamage = 0;

    // Weapon modifiers
    // Light attack mod
    // Heavy attack mod
    // Crit mod

    [Header("Weapon Base Poise Damage")]
    public int basePoiseDamage = 10;
    // Offensive poise bonus when attacking

    [Header("Stamina Costs")]
    public int baseStaminaCost = 20;
    // Running attack cost
    // Light attack cost
    // Heavy attack cost

    [Header("Actions")]
    public WeaponItemAction oh_RB_Action; // One hand right bumper action

    // Ash of war (idk man probably not)

    
    
}
