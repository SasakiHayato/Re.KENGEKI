using UnityEngine;
using BehaviourTree.Execute;

public class ActionIdle : BehaviourAction
{
    EnemyRetentionData _retentionData;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _retentionData = user.GetComponent<EnemyRetentionData>();
    }

    protected override bool Execute()
    {
        _retentionData.MoveDir = Vector3.zero;

        return true;
    }
}
