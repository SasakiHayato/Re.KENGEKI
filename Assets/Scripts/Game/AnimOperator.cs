using UnityEngine;
using MonoState.Data;

/// <summary>
/// �A�j���[�V�����̐���N���X
/// </summary>

public class AnimOperator : MonoBehaviour, IRetentionData
{
    [SerializeField] Animator _anim;

    /// <summary>
    /// Anim.Play();
    /// </summary>
    /// <param name="stateName">�A�j���[�V�����̖��O</param>
    public void PlayRequest(string stateName)
    {
        _anim.Play(stateName);
    }

    /// <summary>
    /// Anim.CrossFade();
    /// </summary>
    /// <param name="stateName">�A�j���[�V�����̖��O</param>
    /// <param name="duration">�t�F�[�h���鎞��</param>
    public void PlayRequest(string stateName, float duration)
    {
        _anim.CrossFade(stateName, duration);
    }



    // ���L, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public Object RetentionData()
    {
        return this;
    }
}
