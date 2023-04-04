using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoStatsData", menuName = "ScriptableObjects/AmmoStatsData", order = 1)]
public class AmmoStatsData : ScriptableObject
{
   public AmmoType ammoType;
   public Sprite WeaponSprite;
   public GameObject AmmoPrefab;
   public float MinDamage;
   public float MaxDamage;
   public float Accuracy;
   public float ShieldBonusDamage;
   public float ArmorBonusDamage;
   public float HitPointBonusDamage;
   public float ArmorPenetration;
   public float AmmoSpeed;
   public float CritChance;
   public float RPS;
   public float ReloadingTime;
   public int AmmoCount;

   public List<DistanceDependStats> DistanceDepend = new List<DistanceDependStats>();
   
   [System.Serializable]
   public class DistanceDependStats
   {
      public Distance distance;
      public float AccuracyMod;
      public float CritChanceMod;
      public float DamageMod;
   }
}

public enum AmmoType
{
   Bullet, Ray, Rocket, Splash
}

