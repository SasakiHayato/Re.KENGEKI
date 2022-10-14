using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;
using MonoState.Data;

public class PlayerStateAttack : MonoStateBase
{
    PlayerRetentionData _retentionData;
    AnimOperator _animOperator;
    AttackController _attackController;

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
        _attackController = UserRetentionData.GetData<AttackController>(nameof(AttackController));
    }

    public override void OnEnable()
    {
        _attackController.Request();
        _animOperator
            .AttributeWaitAnim()
            .PlayRequest(_attackController.CurrentStateName, AnimOperator.PlayType.Fade, CharacterBase.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_attackController.OnNext)
        {
            OnEnable();
            return Player.State.Attack;
        }

        if (_animOperator.IsEndCurrentAnim)
        {
            _retentionData.OnAttack = false;
            _attackController.Cancel();

            return Player.State.Idle;
        }
        else
        {
            return Player.State.Attack;
        }
    }
}
