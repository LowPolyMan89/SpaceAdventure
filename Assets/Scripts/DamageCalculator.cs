using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    public static CalculatedDamage GetCalculatedDamage(Stats targetShipStats, AmmoStatsData ammoStatsData, Distance currentDistance, Ship attackedShip, Ship damagingShip)
    {
        CalculatedDamage _calculatedDamage = new CalculatedDamage();
        
         Distance distance = currentDistance;

         float AccuracyMod = 1;
         float CritChanceMod = 1;
         float DamageMod = 1;

         _calculatedDamage.AttackedShip = attackedShip;
         _calculatedDamage.DamagingShip = damagingShip;
         
         switch (distance)
         {
             case Distance.Long:
                 AccuracyMod = ammoStatsData.DistanceDepend[0].AccuracyMod;
                 CritChanceMod = ammoStatsData.DistanceDepend[0].CritChanceMod;
                 DamageMod = ammoStatsData.DistanceDepend[0].DamageMod;
                 break;
             case Distance.Medium:
                 AccuracyMod = ammoStatsData.DistanceDepend[1].AccuracyMod;
                 CritChanceMod = ammoStatsData.DistanceDepend[1].CritChanceMod;
                 DamageMod = ammoStatsData.DistanceDepend[1].DamageMod;
                 break;
             case Distance.Close:
                 AccuracyMod = ammoStatsData.DistanceDepend[2].AccuracyMod;
                 CritChanceMod = ammoStatsData.DistanceDepend[2].CritChanceMod;
                 DamageMod = ammoStatsData.DistanceDepend[2].DamageMod;
                 break;
         }
   

        _calculatedDamage.isMissing = CalculateChance(ammoStatsData.Accuracy * AccuracyMod - targetShipStats.Evasion);

        if (ammoStatsData.ammoType == AmmoType.Rocket)
            _calculatedDamage.isMissing = false;

        if (_calculatedDamage.isMissing) return _calculatedDamage;
        
        _calculatedDamage.isCrit = CalculateChance(ammoStatsData.CritChance * CritChanceMod);

        float damage =  CalculateMinMaxValue(ammoStatsData.MinDamage, ammoStatsData.MaxDamage);
        damage = _calculatedDamage.isCrit ? damage * 2 : damage;
        damage *= DamageMod;
        
        _calculatedDamage.ShieldDamage = targetShipStats.Shield > 0 ? damage + ammoStatsData.ShieldBonusDamage : 0;
        
        if (_calculatedDamage.ShieldDamage > 0)
        {
            return _calculatedDamage;
        }
        _calculatedDamage.ArmorDamage = targetShipStats.Armor > 0 ? CalculateArmoredDefenceDamage(targetShipStats.ArmorDefence, damage, ammoStatsData.ArmorPenetration) + ammoStatsData.ArmorBonusDamage : 0;
        
        if (_calculatedDamage.ArmorDamage > 0)
        {
            return _calculatedDamage;
        }
        _calculatedDamage.HitPointDamage = targetShipStats.HitPoint > 0 ? damage + ammoStatsData.HitPointBonusDamage : 0;
        
        if (_calculatedDamage.HitPointDamage > 0)
        {
            return _calculatedDamage;
        }
        return _calculatedDamage;
    }

    public static bool CalculateChance(float value)
    {
        float result = Random.Range(0f, 1f);
        //Debug.Log("Random result " + result + " chance value: " + value);
        return  result >= value;
    }
    
    public static float CalculateMinMaxValue(float minValue, float maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    public static float CalculateArmoredDefenceDamage(float armDefence, float damageValue, float armPenetration)
    {
        float calcDefence = armDefence - armPenetration;
        calcDefence = Mathf.Clamp(calcDefence, 0.001f, 0.999f);
        calcDefence = 1 - calcDefence;
        float damage = damageValue * calcDefence;
        return damage;
    }
}



public struct CalculatedDamage
{
    public float HitPointDamage;
    public float ShieldDamage;
    public float ArmorDamage;
    public bool isCrit;
    public bool isMissing;
    public Ship AttackedShip;
    public Ship DamagingShip;
}
