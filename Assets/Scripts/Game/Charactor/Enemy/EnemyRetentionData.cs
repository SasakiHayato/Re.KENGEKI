using UnityEngine;
using MonoState.Data;

public class EnemyRetentionData : MonoBehaviour, IRetentionData
{
    Vector3 _moveDir;
    public Vector3 MoveDir
    {
        get
        {
            return _moveDir.normalized;
        }
        set
        {
            _moveDir = value;
        }
    }

    // ‰º‹L, IRetentionData
    public string RetentionPath => nameof(EnemyRetentionData);

    public Object RetentionData()
    {
        return this;
    }
}
