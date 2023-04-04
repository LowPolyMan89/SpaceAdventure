using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffCuntroller))]
public class Stats : MonoBehaviour
{
   [SerializeField] private ShipStatsData shipStatsData;
   [SerializeField] private BuffCuntroller _buffCuntroller;
   
   public float HitPoint;
   public float Armor;
   public float ArmorDefence;
   public float Shield;
   public float Speed;
   public float Evasion;

   public ShipStatsData StatsData => shipStatsData;

   public BuffCuntroller BuffCuntroller => _buffCuntroller;

   private void Awake()
   {
      _buffCuntroller = gameObject.GetComponent<BuffCuntroller>();
   }

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
[System.Serializable]
public class Stat
{
   public enum StatType
   {
      
   }
}
