using UnityEngine;
using MonoState.Data;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

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

    Action _action;

    bool _attributeWaitAnim;
    bool _attributeCallBack;

    public bool IsEndCurrentAnim { get; private set; }

    public int GetAnimFrameCount(string stateName)
    {
        RuntimeAnimatorController runtime = _anim.runtimeAnimatorController;
        AnimationClip clip = runtime.animationClips.First(r => r.name.Equals(stateName));
        //Debug.Log($"Clip{clip.length}");
        return (int)(clip.length * clip.frameRate);
    }

    /// <summary>
    /// Anim.CrossFade();
    /// </summary>
    /// <param name="stateName">アニメーションの名前</param>
    /// <param name="duration">フェードする時間</param>
    public void PlayRequest(string stateName, PlayType type = PlayType.Onece, float duration = 0)
    {
        if (_attributeWaitAnim)
        {
            EndAnim(duration).Forget();
        }

        if (_attributeCallBack)
        {
            CallBack(duration).Forget();
        }

        if (type == PlayType.Onece)
        {
            _anim.Play(stateName);
        }
        else
        {
            _anim.CrossFade(stateName, duration);
        }

        Initalize();
    }

    void Initalize()
    {
        _attributeWaitAnim = false;
        _attributeCallBack = false;
    }

    public AnimOperator AttributeWaitAnim()
    {
        _attributeWaitAnim = true;

        return this;
    }

    public AnimOperator AttaributeCallBack(Action action)
    {
        _action += action;
        _attributeCallBack = true;

        return this;
    }

    public void SetRootMotion(bool apply)
    {
        _anim.applyRootMotion = apply;
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

    async UniTask CallBack(float duration)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        // Unity側の関係で1フレーム待つ
        await UniTask.DelayFrame(1);
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _action?.Invoke();
    }

    // 下記, IRetentionData
    public string RetentionPath => nameof(AnimOperator);

    public UnityEngine.Object RetentionData()
    {
        return this;
    }
}
