using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackInfomation 
{
    [SerializeField] int _isActiveFrame;
    [SerializeField] int _endActiveFreme;
    [SerializeField] int _attributeNextFeme;
    [SerializeField] List<AttackEffectType> _effectTypeList;

    public int IsActiveFrame => _isActiveFrame;
    public int EndActiveFreme => _endActiveFreme;
    public int AttributeNextFreme => _attributeNextFeme;
    public List<AttackEffectType> HitEffectTypeList => _effectTypeList;
}
