using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
   [SerializeField] private ShipStatsData shipStatsData;
   
   public float HitPoint;
   public float Armor;
   public float ArmorDefence;
   public float Shield;
   public float Speed;
   public float Evasion;

   public ShipStatsData StatsData => shipStatsData;

   private void Start()
   {
      HitPoint = StatsData.HitPoint;
      Armor = StatsData.Armor;
      ArmorDefence = StatsData.ArmorDefence;
      Shield = StatsData.Shield;
      Evasion = StatsData.Evasion;
      Speed = StatsData.Speed;
   }
}
