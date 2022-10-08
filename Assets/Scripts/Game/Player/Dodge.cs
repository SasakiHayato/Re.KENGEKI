using UnityEngine;
using MonoState.State;
using System;

public class Dodge : MonoStateBase
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

        _animOperator.PlayRequest(stateName, AnimOperator.PlayType.Fade, Player.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (true)
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
