using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SSBuffController))]
public class SStatController : MonoBehaviour
{
   [SerializeField] private List<SStat> _stats = new List<SStat>();

   public List<SStat> Stats => _stats;

   private void Awake()
   {
      foreach (var stat in _stats)
      {
         stat.Init();
      }
   }

   private List<SBuff> FindBuffList(STypes.SStatType type)
   {
      foreach (var stat in _stats)
      {
         if (stat.StatType == type)
         {
            return stat.SStatBuffs;
         }
      }

      return null;
   }

   private void FindBuffInStatsAndRemove(SBuff buff)
   {
      var vs = FindBuffList(buff.BuffData.BuffedStat);
      
      for (var index = 0; index < vs.Count; index++)
      {
         var v = vs[index];
         vs.RemoveAt(index);
         return;
      }
   }

   public void RemoveBuff(SBuff buff)
   {
      FindBuffInStatsAndRemove(buff);
   }
}
