using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour, IDamagable
{
    [SerializeField] private Ship _ship;

    public CalculatedDamage TakeDamage(AmmoStatsData ammoStatsData, Ship attackerShip)
    {
        CalculatedDamage calc = DamageCalculator.GetCalculatedDamage(_ship.ShipStats, ammoStatsData, Battle.Instance.CurrentDistanceType, attackerShip, _ship);
        _ship.TakeDamage(calc);
        Events.OnTakeDamageAction(calc);
        if(calc.isMissing)
            print("Miss");
        return calc;
    }

    public Transform GetSelfTransform()
    {
        return _ship.transform;
    }
}
