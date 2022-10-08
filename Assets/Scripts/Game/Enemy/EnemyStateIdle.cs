using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;

public class EnemyStateIdle : MonoStateBase
{
    AnimOperator _animOperator;

    public override void Setup()
    {
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
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
        return Enemy.State.Idle;
    }
}
