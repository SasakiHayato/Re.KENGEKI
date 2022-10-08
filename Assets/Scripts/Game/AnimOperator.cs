using UnityEngine;
using MonoState.Data;

/// <summary>
/// アニメーションの制御クラス
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
    /// <param name="stateName">アニメーションの名前</param>
    /// <param name="duration">フェードする時間</param>
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

    // 下記, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public Object RetentionData()
    {
        return this;
    }
}
