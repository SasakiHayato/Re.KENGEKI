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

    int _stateIndex;
    string[] _stateNameArray = new string[] {"Attack1", "Attack2" ,"Attack3" };
    string[] _stateNameCounter = new string[] { "Counter_Slash", "Counter_Attract" };

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<PlayerRetentionData>(nameof(PlayerRetentionData));
        _animOperator = UserRetentionData.GetData<AnimOperator>(nameof(AnimOperator));
        _attackController = UserRetentionData.GetData<AttackController>(nameof(AttackController));
    }

    public override void OnEnable()
    {
        if (_stateIndex >= _stateNameArray.Length)
        {
            _stateIndex = 0;
        }

        string stateName;

        if (_retentionData.CounterID != int.MinValue)
        {
            stateName = _stateNameCounter[_retentionData.CounterID];
        }
        else
        {
            stateName = _stateNameArray[_stateIndex];
        }

        int frameCount = _animOperator.GetAnimFrameCount(stateName);
        _attackController.Request(frameCount);

        _animOperator
            .AttributeWaitAnim()
            .PlayRequest(stateName, AnimOperator.PlayType.Fade, CharacterBase.AnimDuration);
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        if (_retentionData.OnDodge)
        {
            Initalize();
            return Player.State.Dodge;
        }

        if (_attackController.OnNext && _retentionData.OnNextAttack)
        {
            _retentionData.OnNextAttack = false;
            _stateIndex++;
            OnEnable();
            return Player.State.Attack;
        }

        if (_animOperator.IsEndCurrentAnim)
        {
            Initalize();
            return Player.State.Idle;
        }
        else
        {
            return Player.State.Attack;
        }
    }

    void Initalize()
    {
        _retentionData.OnAttack = false;
        _retentionData.OnNextAttack = false;
        _retentionData.CounterID = int.MinValue;
        _attackController.Cancel();
        _stateIndex = 0;
    }
}
