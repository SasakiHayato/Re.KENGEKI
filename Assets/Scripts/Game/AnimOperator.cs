using UnityEngine;
using MonoState.Data;

/// <summary>
/// アニメーションの制御クラス
/// </summary>

public class AnimOperator : MonoBehaviour, IRetentionData
{
    [SerializeField] Animator _anim;

    /// <summary>
    /// Anim.Play();
    /// </summary>
    /// <param name="stateName">アニメーションの名前</param>
    public void PlayRequest(string stateName)
    {
        _anim.Play(stateName);
    }

    /// <summary>
    /// Anim.CrossFade();
    /// </summary>
    /// <param name="stateName">アニメーションの名前</param>
    /// <param name="duration">フェードする時間</param>
    public void PlayRequest(string stateName, float duration)
    {
        _anim.CrossFade(stateName, duration);
    }



    // 下記, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public Object RetentionData()
    {
        return this;
    }
}
