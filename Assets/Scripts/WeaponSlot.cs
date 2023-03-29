using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Weapon _mountedWeapon;

    public Weapon MountedWeapon => _mountedWeapon;

    public void Init(Ship ship)
    {
       _mountedWeapon.Init(ship);
    }
}
