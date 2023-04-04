using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SStat
{
    private STypes.SStatType _statType;
    public SStatValueData SStatValue;
    [SerializeField] private float _currentValue;
    [SerializeField] private float _maxValue;
    private string _id;

    [SerializeField] private List<SBuff> _sStatBuffs = new List<SBuff>();

    public STypes.SStatType StatType => _statType;

    public float CurrentValue => _currentValue;

    public float MaxValue => _maxValue;

    public string ID => _id;

    public List<SBuff> SStatBuffs => _sStatBuffs;

    public void AddBuff(SBuff buff)
    {
        _sStatBuffs.Add(buff);
    }

    public void Init()
    {
        _id = SStatValue.StatId;
        _statType = SStatValue.StatType;
        _maxValue = SStatValue.MaxValue;
        _currentValue = _maxValue;
    }

    public void DoBuff(SBuff sBuff)
    {
        switch (sBuff.BuffData.ValueModifi)
        {
            case STypes.SBuffValueModifi.Add:

                switch (sBuff.BuffData.ValueType)
                {

                    case STypes.SValueType.Current:
                        _currentValue += sBuff.BuffData.BuffValue;
                        if (_currentValue > _maxValue)
                        {
                            _currentValue = _maxValue;
                        }

                        break;
                    case STypes.SValueType.Maximum:
                        _maxValue += sBuff.BuffData.BuffValue;
                        break;
                }

                break;
            case STypes.SBuffValueModifi.Mull:

                switch (sBuff.BuffData.ValueType)
                {
                    case STypes.SValueType.Current:
                        _currentValue *= sBuff.BuffData.BuffValue;
                        if (_currentValue > _maxValue)
                        {
                            _currentValue = _maxValue;
                        }

                        break;
                    case STypes.SValueType.Maximum:
                        _maxValue *= sBuff.BuffData.BuffValue;
                        break;
                }

                break;
            case STypes.SBuffValueModifi.Set:
                
                switch (sBuff.BuffData.ValueType)
                {
                    case STypes.SValueType.Current:
                        _currentValue = sBuff.BuffData.BuffValue;
                        if (_currentValue > _maxValue)
                        {
                            _currentValue = _maxValue;
                        }

                        break;
                    case STypes.SValueType.Maximum:
                        _maxValue = sBuff.BuffData.BuffValue;
                        break;
                }
                break;

        }
    }

    public void EndBuff(SBuff sBuff)
    {
        if (sBuff.CashedValue > 0)
        {
            _maxValue = sBuff.CashedValue;
        }
    }
}
