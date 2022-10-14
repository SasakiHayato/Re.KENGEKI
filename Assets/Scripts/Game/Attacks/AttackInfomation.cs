using UnityEngine;

[System.Serializable]
public class AttackInfomation 
{
    [SerializeField] int _isActiveFrame;
    [SerializeField] int _endActiveFreme;
    [SerializeField] int _attributeNextFeme;

    public int IsActiveFrame => _isActiveFrame;
    public int EndActiveFreme => _endActiveFreme;
    public int AttributeNextFreme => _attributeNextFeme;
}
