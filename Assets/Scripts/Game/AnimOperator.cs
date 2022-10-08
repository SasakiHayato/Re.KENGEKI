using UnityEngine;
using MonoState.Data;

/// <summary>
/// �A�j���[�V�����̐���N���X
/// </summary>

public class AnimOperator : MonoBehaviour, IRetentionData
{
    public enum PlayType
    {
        Onece,
        Fade,
    }

    [SerializeField] Animator _anim;

    /// <summary>
    /// Anim.CrossFade();
    /// </summary>
    /// <param name="stateName">�A�j���[�V�����̖��O</param>
    /// <param name="duration">�t�F�[�h���鎞��</param>
    public void PlayRequest(string stateName, PlayType type = PlayType.Onece, float duration = 0)
    {
        if (type == PlayType.Onece)
        {
            _anim.Play(stateName);
        }
        else
        {
            _anim.CrossFade(stateName, duration);
        }
    }

    // ���L, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public Object RetentionData()
    {
        return this;
    }
}
