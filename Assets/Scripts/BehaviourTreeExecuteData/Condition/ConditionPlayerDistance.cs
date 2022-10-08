using UnityEngine;
using BehaviourTree.Execute;

public class ConditionPlayerDistance : BehaviourConditional
{
    enum TrueType
    {
        In,
        Out,
    }

    [SerializeField] TrueType _trueType;
    [SerializeField] float _attributeDistance;

    protected override bool Try()
    {
        Transform player = GameManager.Instance.GameUser;
        float dist = Vector3.Distance(User.transform.position, player.position);
        
        if (_trueType == TrueType.In)
        {
            return dist < _attributeDistance;
        }
        else
        {
            return dist > _attributeDistance;
        }
    }
}
