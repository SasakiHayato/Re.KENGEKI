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
        
        AttackType attackType;
        string stateName;

        if (_retentionData.CounterID != int.MinValue)
        {
            attackType = AttackType.Special;
            stateName = _stateNameCounter[_retentionData.CounterID];
        }
        else
        {
            attackType = AttackType.Usually;
            stateName = _stateNameArray[_stateIndex];
        }
        
        int frameCount = _animOperator.GetAnimFrameCount(stateName);

        if (attackType == AttackType.Special)
        {
            _attackController.RequestAt(attackType, _retentionData.CounterID, frameCount);
        }
        else
        {
            _attackController.Request(attackType, frameCount);
        }
        
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
            _stateIndex++;
            _retentionData.OnNextAttack = false;

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
