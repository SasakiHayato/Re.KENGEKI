using UnityEngine;
using MonoState.State;
using System;

public class PlayerStateDodge : MonoStateBase
{
    Vector2 _inputDir;

    PlayerRetentionData _retentionData;
    AnimOperator _animOperator;

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEnable()
    {
        // “ü—Í‚ª‚È‚¯‚ê‚ÎPlayer‚ÌŒã‚ë•ûŒü‚ð‘ã“ü
        if (_retentionData.ReadInputDir == Vector2.zero)
        {
            Vector3 back = UserRetentionData.User.transform.forward * -1;
            _inputDir = new Vector2(back.x, back.z);
        }
        else
        {
            _inputDir = _retentionData.ReadInputDir;
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
            .AttributeWaitAnim()
            .PlayRequest(stateName, AnimOperator.PlayType.Fade, CharacterBase.AnimDuration);
    }

    public override void Execute()
    {
        _retentionData.SetInputDir(_inputDir);
    }

    public override Enum Exit()
    {
        if (_animOperator.IsEndCurrentAnim)
        {
            _retentionData.OnDodge = false;
            return Player.State.Idle;
        }
        else
        {
            return Player.State.Dodge;
        }
    }
}
