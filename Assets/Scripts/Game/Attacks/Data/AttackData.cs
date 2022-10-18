using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Usually,
    Special,
}

public enum AttackEffectType
{
    HitStop,
    Particle,
    ShakeCm,
}

[System.Serializable]
public class AttackData
{
    [SerializeField] AttackType _attackType;
    [SerializeField] List<AttackInfomation> _attackInfomationList;

    public int DataCount => _attackInfomationList.Count;
    public AttackType AttackType => _attackType;
    public AttackInfomation GetInfo(int index)
    {
        return _attackInfomationList[index];
    }
}
