using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipStatsData", menuName = "ScriptableObjects/ShipStatsData", order = 1)]
public class ShipStatsData : ScriptableObject
{
   public Sprite ShipSprite;
   public float HitPoint;
   public float Armor;
   public float ArmorDefence;
   public float Shield;
   public float Speed;
   public float Evasion;
}
