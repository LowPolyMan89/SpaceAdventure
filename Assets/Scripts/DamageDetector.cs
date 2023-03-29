using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour, IDamagable
{
    [SerializeField] private Ship _ship;

    public CalculatedDamage TakeDamage(AmmoStatsData ammoStatsData)
    {
        CalculatedDamage calc = DamageCalculator.GetCalculatedDamage(_ship.ShipStats, ammoStatsData, Battle.Instance.CurrentDistanceType);
        _ship.TakeDamage(calc);
        if(calc.isMissing)
            print("Miss");
        return calc;
    }

    public Transform GetSelfTransform()
    {
        return _ship.transform;
    }
}
