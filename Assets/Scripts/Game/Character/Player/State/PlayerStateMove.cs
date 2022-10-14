using UnityEngine;
using MonoState.State;
using System;

public class PlayerStateMove : MonoStateBase
{
    PlayerRetentionData _retentionData;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEnable()
    {
        _animOperator.PlayRequest("Run",AnimOperator.PlayType.Fade, Player.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_retentionData.OnAttack)
        {
            return Player.State.Attack;
        }

        if (_retentionData.OnDodge)
        {
            return Player.State.Dodge;
        }

        if (_retentionData.ReadOnMove)
        {
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
