using UnityEngine;
using MonoState.Data;
using System;
using Cysharp.Threading.Tasks;

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

    public bool IsEndCurrentAnim { get; private set; }

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

    public AnimOperator AttributeWaitAnim(float duration)
    {
        EndAnim(duration).Forget();
        return this;
    }

    async UniTask EndAnim(float duration)
    {
        IsEndCurrentAnim = false;
        
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        // Unity側の関係で1フレーム待つ
        await UniTask.DelayFrame(1);
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        IsEndCurrentAnim = true;
    }

    // 下記, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public UnityEngine.Object RetentionData()
    {
        return this;
    }
}
