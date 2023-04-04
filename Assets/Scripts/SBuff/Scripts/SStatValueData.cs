using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SStatValueData", menuName = "ScriptableObjects/SStatValueData", order = 1)]
public class SStatValueData : ScriptableObject
{
   public string StatId;
   public STypes.SStatType StatType;
   public float MaxValue;
   public float ValuePerTime;
}
