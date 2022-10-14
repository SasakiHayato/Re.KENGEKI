using UnityEngine;
using MonoState.Data;

/// <summary>
/// Playerのステートマシン保持データクラス
/// </summary>

public class PlayerRetentionData : MonoBehaviour, IRetentionData
{
    /// <summary>
    /// trueなら行動中
    /// </summary>
    public bool ReadOnMove
    {
        get
        {
            Vector2 dir = ReadInputDir;

            if (Mathf.Abs(dir.x) <= 0 && Mathf.Abs(dir.y) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool OnDodge { get; set; }

    public bool OnAttack { get; set; }

    public bool OnNextAttack { get; set; }

    public Vector2 ReadInputDir { get; private set; }

    public void SetInputDir(Vector2 dir)
    {
        ReadInputDir = dir;
    }

    // 下記, IRetentionData
    public string RetentionPath => nameof(PlayerRetentionData);

    public Object RetentionData()
    {
        return this;
    }
}
