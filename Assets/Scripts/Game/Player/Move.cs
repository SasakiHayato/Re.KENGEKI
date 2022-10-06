using UnityEngine;
using MonoState.State;
using System;

public class Move : MonoStateBase
{
    Vector3 _beforePos;

    AnimOperator _animOperator;

    public override void Setup()
    {
        _animOperator = UserRetentionData.GetRetentionData<AnimOperator>(nameof(AnimOperator));
        _beforePos = UserRetentionData.User.transform.position;
    }

    public override void OnEnable()
    {
        _animOperator.PlayRequest("", 0.2f);
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
