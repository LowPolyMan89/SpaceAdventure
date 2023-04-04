using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SBuffData", menuName = "ScriptableObjects/SBuffData", order = 2)]
public class SBuffData : ScriptableObject
{
    public string BuffId;
    public STypes.SStatType BuffedStat;
    public float BuffValue;
    public STypes.SBuffValueModifi ValueModifi;
    public STypes.SValueType ValueType;
    public STypes.Period Period;
    [Min(1)]
    public float BuffTime;
    public bool IsPermanent;
    public STypes.SBuffStackType StackType;
    public string BuffCondition;
    public SBuffData ConditionBuffData;
    public Sprite BuffImage;
}
