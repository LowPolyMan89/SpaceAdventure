using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SStatController))]
public class SSBuffController : MonoBehaviour
{
   [SerializeField] private List<SBuff> _allBuffs = new List<SBuff>();
   [SerializeField] private SBuffDatabase _buffDatabase;
   private SStatController _statController;

   private void Awake()
   {
      _statController = gameObject.GetComponent<SStatController>();
   }
   
   private void Update()
   {
      if(_allBuffs.Count < 1) return;
      
      for (var index = 0; index < _allBuffs.Count; index++)
      {
         var buff = _allBuffs[index];

         if (!buff.BuffData.IsPermanent)
         {
            if (buff.UpdateBuff(Time.deltaTime) <= 0)
            {
               _allBuffs.RemoveAt(index);
               _statController.RemoveBuff(buff);
            }
         }
      }
   }

   [ContextMenu("SmallMedpack")]
   public void Test1()
   {
      AddBuff("SmallMedpack");
   }
   
   [ContextMenu("SmallMedpackss")]
   public void Test2()
   {
      AddBuff("SmallMedpackss");
   }
   
   public void AddBuff(string buffId)
   {
      var newBuffData = _buffDatabase.GetBuff(buffId);
      
      if (newBuffData == null)
      {
         Debug.LogWarning("Cant create new buff with id: " + buffId);
         return;
      }
      
      foreach (var stat in _statController.Stats)
      {
         if (stat.StatType == newBuffData.BuffedStat)
         {
            var newBuff = new SBuff(newBuffData, stat);
            _allBuffs.Add(newBuff);
            stat.AddBuff(newBuff);
            return;
         }
         else
         {
            Debug.LogWarning("Cant find stat with: " + stat.StatType);
         }
      }
   }
   
}
