using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelInstatiationSlot : MonoBehaviour
{
    public WeaponModelSlot weaponSlot;
    public GameObject currentWeaponModel;

    public void UnloadWeapon()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeapon(GameObject weaponModel)
    {
        currentWeaponModel = weaponModel;
        weaponModel.transform.parent = transform;

        weaponModel.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weaponModel.transform.localScale = Vector3.one;
    }
}
