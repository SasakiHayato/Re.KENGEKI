using UnityEngine;
using MonoState.Data;

/// <summary>
/// Player�̃X�e�[�g�}�V���ێ��f�[�^�N���X
/// </summary>

public class PlayerRetentionData : MonoBehaviour, IRetentionData
{
    /// <summary>
    /// true�Ȃ�s����
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

    // ���L, IRetentionData
    public string RetentionPath => nameof(PlayerRetentionData);

    public Object RetentionData()
    {
        return this;
    }
}
