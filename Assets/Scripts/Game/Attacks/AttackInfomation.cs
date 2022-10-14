using UnityEngine;

[System.Serializable]
public class AttackInfomation 
{
    [SerializeField] string _stateName;
    [SerializeField] int _isActiveFrame;
    [SerializeField] int _endActiveFreme;
    [SerializeField] int _attributeNextFeme;

    public string StateName => _stateName;
    public int IsActiveFrame => _isActiveFrame;
    public int EndActiveFreme => _endActiveFreme;
    public int AttributeNextFreme => _attributeNextFeme;
}
