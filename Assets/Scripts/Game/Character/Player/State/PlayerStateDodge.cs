using UnityEngine;
using MonoState.State;
using System;

public class PlayerStateDodge : MonoStateBase
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
        float x = _playerRetention.ReadInputDir.x;
        
        string stateName;

        if (x > 0)
        {
            stateName = "Dodge_Right";
        }
        else
        {
            stateName = "Dodge_Left";
        }

        _animOperator
            .AttributeWaitAnim(ChatracterBase.AnimDuration)
            .PlayRequest(stateName, AnimOperator.PlayType.Fade, ChatracterBase.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_animOperator.IsEndCurrentAnim)
        {
            _playerRetention.OnDodge = false;
            return Player.State.Idle;
        }
        else
        {
            return Player.State.Dodge;
        }
    }
}
