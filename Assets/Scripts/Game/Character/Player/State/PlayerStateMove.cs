using UnityEngine;
using MonoState.State;
using System;

public class PlayerStateMove : MonoStateBase
{
    PlayerRetentionData _playerRetention;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _playerRetention = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
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
        if (_playerRetention.OnDodge)
        {
            return Player.State.Dodge;
        }

        if (_playerRetention.ReadOnMove)
        {
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
