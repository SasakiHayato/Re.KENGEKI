using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;

public class EnemyStateIdle : MonoStateBase
{
    EnemyRetentionData _retentionData;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
        _retentionData = UserRetentionData.GetData<EnemyRetentionData>(nameof(EnemyRetentionData));
    }

    public override void OnEnable()
    {
        _animOperator.PlayRequest("Idle", AnimOperator.PlayType.Fade, Enemy.AnimDuration);
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
