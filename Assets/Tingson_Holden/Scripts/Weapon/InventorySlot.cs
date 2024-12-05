using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public WeaponInfo _weaponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }

    public void SetWeaponInfo(WeaponInfo newWeaponInfo)
    {
        _weaponInfo = newWeaponInfo;
    }
}
