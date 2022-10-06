using UnityEngine;

/// <summary>
/// アニメーションの制御クラス
/// </summary>

public class AnimOperator : MonoBehaviour
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
    public void PlayRequest(string stateName, float duration = 0.2f)
    {
        _anim.CrossFade(stateName, duration);
    }
}
