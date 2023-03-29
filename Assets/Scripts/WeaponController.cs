using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponSlot> _weaponSlots = new List<WeaponSlot>();

    public List<WeaponSlot> WeaponSlots => _weaponSlots;

    public void SetTargetToWeapon(int indx, Transform target)
    {
        WeaponSlots[indx].MountedWeapon.AddTarget(target);
    }
}
