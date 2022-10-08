using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;

public class EnemyStateAttack : MonoStateBase
{
    EnemyRetentionData _retentionData;
    BulletOperator _bulletOperator;

    public override void Setup()
    {
        _retentionData = UserRetentionData.GetData<EnemyRetentionData>(nameof(EnemyRetentionData));
        _bulletOperator = UserRetentionData.GetData<BulletOperator>(nameof(BulletOperator));
    }

    public override void OnEnable()
    {
        _bulletOperator.ShotRequest();
    }

    public override void Execute()
    {
        
    }

    public override Enum Exit()
    {
        return Enemy.State.Attack;
    }
}
