using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

public class ActionAttack : BehaviourAction
{
    EnemyRetentionData _retentionData;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _retentionData = user.GetComponent<EnemyRetentionData>();
    }

    protected override bool Execute()
    {
        _retentionData.OnAttack = true;
        return true;
    }
}
