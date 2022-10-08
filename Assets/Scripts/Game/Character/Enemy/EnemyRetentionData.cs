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

    /// <summary>
    /// çUåÇíÜÇ»ÇÁTrue
    /// </summary>
    public bool OnAttack { get; set; }

    // â∫ãL, IRetentionData
    public string RetentionPath => nameof(EnemyRetentionData);

    public Object RetentionData()
    {
        return this;
    }
}
