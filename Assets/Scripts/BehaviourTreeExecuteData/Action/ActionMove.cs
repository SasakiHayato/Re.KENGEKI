using UnityEngine;
using BehaviourTree.Execute;

public class ActionMove : BehaviourAction
{
    enum MoveType
    {
        TowardPlayer,
        AwayPlayer,
    }

    [SerializeField] MoveType _moveType;

    EnemyRetentionData _retentionData;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _retentionData = user.GetComponent<EnemyRetentionData>();
    }

    protected override bool Execute()
    {
        Transform player = GameManager.Instance.GameUser;

        Vector3 dir;

        if (_moveType == MoveType.TowardPlayer)
        {
            dir = player.position - User.transform.position;
        }
        else
        {
            dir = User.transform.position - player.position;
        }

        _retentionData.MoveDir = dir;

        return true;
    }
}
