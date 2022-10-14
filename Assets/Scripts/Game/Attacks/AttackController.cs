using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MonoState.Data;

public class AttackController : MonoBehaviour, IRetentionData
{
    [SerializeField] Weapon _weapon;
    [SerializeField] List<AttackInfomation> _infomationList;

    int _infoIndex;

    public string CurrentStateName { get; private set; }

    public bool OnNext { get; private set; }

    public void Request()
    {
        if (_infoIndex >= _infomationList.Count)
        {
            _infoIndex = 0;
        }

        AttackInfomation info = _infomationList[_infoIndex];
        CurrentStateName = info.StateName;
        _weapon.Active(true);

        OnNext = false;
        OnProcess(info).Forget();
    }

    public void Cancel()
    {
        _weapon.ColliderEnable(false);
        _weapon.Active(false);
        _infoIndex = 0;

        OnNext = false;
    }

    async UniTask OnProcess(AttackInfomation infomation)
    {
        int frame = 0;

        await UniTask.WaitUntil(() => Check(ref frame, infomation.IsActiveFrame));
        _weapon.ColliderEnable(true);

        await UniTask.WaitUntil(() => Check(ref frame, infomation.EndActiveFreme));
        _weapon.ColliderEnable(false);

        await UniTask.WaitUntil(() => Check(ref frame, infomation.AttributeNextFreme));
        _infoIndex++;

        OnNext = true;
    }

    bool Check(ref int frame, int checkFrame)
    {
        frame++;
        //Debug.Log(frame);
        return frame >= checkFrame;
    }

    // ‰º‹L, IRetentionData
    public string RetentionPath => nameof(AttackController);

    public Object RetentionData()
    {
        return this;
    }
}
