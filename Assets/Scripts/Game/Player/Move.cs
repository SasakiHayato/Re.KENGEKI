using UnityEngine;
using MonoState.State;
using System;

public class Move : MonoStateBase
{
    Player _player;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _player = UserRetentionData.GetData<Player>(nameof(Player));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEnable()
    {
        _animOperator.PlayRequest("Run", 0.1f);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_player.OnMove)
        {
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
