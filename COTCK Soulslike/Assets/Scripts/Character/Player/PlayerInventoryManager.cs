using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public WeaponItem currentRightHandWeapon;
    public WeaponItem currentLeftHandWeapon;

    public ColorPassive currentColorPassive;

    public RangedProjectileItem mainProjectile;

    public bool isPassiveActive = false;

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;

        if (currentSceneName == "World" && !isPassiveActive)
        {
            isPassiveActive = true;
            ActivateColorPassive();
        }
    }

    public void ActivateColorPassive()
    {
        if (currentColorPassive != null)
        {
            if (currentColorPassive is GreenColorPassive greenColorPassive)
            {
                greenColorPassive.SummonPlantAlly(gameObject.transform);
            }
        }
    }
}
