using UnityEngine;
using MonoState.State;
using System;

public class Idle : MonoStateBase
{
    AnimOperator _animOperator;

    Vector3 _beforePos;

    public override void Setup()
    {
        _animOperator = UserRetentionData.GetRetentionData<AnimOperator>(nameof(AnimOperator));
        _beforePos = UserRetentionData.User.transform.position;

    }

    public override void OnEnable()
    {
        
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        Vector3 currentPos = UserRetentionData.User.transform.position;
        if (_beforePos != currentPos)
        {
            _beforePos = currentPos;
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
