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
    /// �U�����Ȃ�True
    /// </summary>
    public bool OnAttack { get; set; }

    // ���L, IRetentionData
    public string RetentionPath => nameof(EnemyRetentionData);

    public Object RetentionData()
    {
        return this;
    }
}
