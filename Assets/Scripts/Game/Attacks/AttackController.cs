using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MonoState.Data;

public class AttackController : MonoBehaviour, IRetentionData
{
    [SerializeField] Weapon _weapon;
    [SerializeField] List<AttackInfomation> _infomationList;

    int _infoIndex;

    public bool OnNext { get; private set; }

    public void Request(int frameCount = 0)
    {
        if (_infoIndex >= _infomationList.Count)
        {
            _infoIndex = 0;
        }

        AttackInfomation info = _infomationList[_infoIndex];
        _weapon.Active(true);

        OnNext = false;
        OnProcess(info, frameCount).Forget();
    }

    public void Cancel()
    {
        _weapon.ColliderEnable(false);
        _weapon.Active(false);
        _infoIndex = 0;

        OnNext = false;
    }

    async UniTask OnProcess(AttackInfomation infomation, int frameCount)
    {
        float timer = 0;

        await UniTask.WaitUntil(() => Check(ref timer, frameCount, infomation.IsActiveFrame));
        _weapon.ColliderEnable(true);
        
        await UniTask.WaitUntil(() => Check(ref timer, frameCount, infomation.EndActiveFreme));
        _weapon.ColliderEnable(false);
        
        await UniTask.WaitUntil(() => Check(ref timer, frameCount, infomation.AttributeNextFreme));
        _infoIndex++;
        
        OnNext = true;
    }

    bool Check(ref float timer, int frameCount, int check)
    {
        timer += Time.deltaTime;

        int count = (int)Mathf.Lerp(0, frameCount, timer);
        return count > check;
    }

    // ‰º‹L, IRetentionData
    public string RetentionPath => nameof(AttackController);

    public Object RetentionData()
    {
        return this;
    }
}
