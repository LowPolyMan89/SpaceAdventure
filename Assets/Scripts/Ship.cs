using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject shipModel;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private DamageDetector damageDetector;
    [SerializeField] private Stats _shipStats;
    [SerializeField] private List<ShipAbility> _abilities = new List<ShipAbility>();

    [SerializeField] private Ai ai;

    public ShipSide Side;
    public Transform selectedTarget;

    public DamageDetector DamageDetector => damageDetector;

    public Stats ShipStats => _shipStats;

    public WeaponController WeaponController => weaponController;

    public Ai AI => ai;

    private void Update()
    {
        AI.Update();
    }

    private void Start()
    {
        ai = new Ai(this);
        if (Side != ShipSide.Player) AI.isEnabled = true;
        Events.OnNewShipSpawnAction(this);
        weaponController.Init(this);
    }

    public void TakeDamage(CalculatedDamage calculated)
    {
        CalculatedDamage calculatedDamage = calculated;
        ShipStats.Shield -= calculatedDamage.ShieldDamage;
        ShipStats.Shield = Mathf.Max(0, ShipStats.Shield);
        
        ShipStats.Armor -= calculatedDamage.ArmorDamage;
        ShipStats.Armor = Mathf.Max(0, ShipStats.Armor);
        
        ShipStats.HitPoint -= calculatedDamage.HitPointDamage;
        ShipStats.HitPoint = Mathf.Max(0, ShipStats.HitPoint);

        if (ShipStats.HitPoint <= 0)
        {
            Events.OnNewShipDestroyAction(this);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class Ai
{
    private Ship _ship;
    public bool isEnabled;

    public void Update()
    {
        if(!isEnabled) return;
        
        if (_ship.selectedTarget == null)
        {
            _ship.selectedTarget = SelectNewTarget(true, null);
        }
    }

    public Transform SelectNewTarget(bool randm, Transform _target)
    {
        Transform target = _target;
        
        for (int i = 0; i < _ship.WeaponController.WeaponSlots.Count; i++)
        {
            if(!target && randm)
                 target = Battle.Instance.GetRandomEnemy(_ship.Side);
            _ship.selectedTarget = target;
            _ship.WeaponController.SetTargetToWeapon(i, target);
        }

        return target;
    }

    public Ai(Ship ship)
    {
        _ship = ship;
    }
}

public enum ShipSide
{
    Player, Enemy
}