using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;

public class EnemyStateMove : MonoStateBase
{
    EnemyRetentionData _retentionData;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<EnemyRetentionData>(nameof(EnemyRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEnable()
    {
        _animOperator.PlayRequest("Run", AnimOperator.PlayType.Fade, Enemy.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_retentionData.MoveDir != Vector3.zero)
        {
            return Enemy.State.Move;
        }
        else
        {
            return Enemy.State.Idle;
        }
    }
}
