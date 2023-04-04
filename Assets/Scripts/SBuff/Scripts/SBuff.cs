using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SBuff
{
    [SerializeField] private string _buffId;
    private SBuffData _buffData;
    [SerializeField] private SStat _sstat;
    [SerializeField] private float _bufftime;
    [SerializeField] private float _deltaTime;
    [SerializeField] private float _cashedValue;
    [SerializeField] private bool isUse;
    public SBuff(SBuffData buffData, SStat stat)
    {
        _buffData = buffData;
        _sstat = stat;
        Init();
    }

    public SBuffData BuffData => _buffData;

    public float BuffTime => _bufftime;

    public float CashedValue => _cashedValue;

    public void Init()
    {
        _bufftime = _buffData.BuffTime;
        _buffId = _buffData.BuffId;
        
        if (_buffData.ValueType == STypes.SValueType.Maximum)
        {
            _cashedValue = _sstat.MaxValue;
        }
    }

    public float UpdateBuff(float deltatime)
    {
        _deltaTime += deltatime;
        
        if (BuffData.Period == STypes.Period.OneTime)
        {
            if (!isUse)
            {
                _sstat.DoBuff(this);
                isUse = true;
            }
        }
        
        if (_deltaTime >= 1.00f)
        {
            _bufftime--;
            _deltaTime = 0;
            
            if (!isUse)
            {
                _sstat.DoBuff(this);
            }

        }

        if (_bufftime <= 0)
        {
            _sstat.EndBuff(this);
        }
        
        return _bufftime;
    }
    
}
