using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STypes
{
public enum SStatType
{
    HitPoint, Armor, Shield, Evasion, Speed, ArmorDefence
}
public enum SBuffValueModifi
{
    Add, Mull, Set
}
public enum SBuffStackType
{
    None, AddTime, AddValue, AddTimeAndValue, Reset, Remove, RemoveAndAddNew
}
public enum SValueType
{
    Current, Maximum
}
public enum Period
{
    OneTime, OneSecond
}
}
