using UnityEngine;
using MonoState.State;
using System;

public class PlayerStateDodge : MonoStateBase
{
    Vector2 _inputDir;

    PlayerRetentionData _playerRetention;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _playerRetention = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEnable()
    {
        if (_playerRetention.ReadInputDir == Vector2.zero)
        {
            Vector3 back = UserRetentionData.User.transform.forward * -1;
            _inputDir = new Vector2(back.x, back.z);
        }
        else
        {
            _inputDir = _playerRetention.ReadInputDir;
        }
        
        string stateName;

        if (_inputDir.x > 0)
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
        _playerRetention.SetInputDir(_inputDir);
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
