using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using MonoState.Data;

public class AttackController : MonoBehaviour, IRetentionData
{
    [SerializeField] Weapon _weapon;
    [SerializeField] List<AttackData> _dataList;

    int _infoIndex;

    public bool OnNext { get; private set; }

    public void Request(AttackType attackType, int frameCount = 0)
    {
        if (_infoIndex >= _dataList.First(d => d.AttackType == attackType).DataCount)
        {
            _infoIndex = 0;
        }

        AttackInfomation info = _dataList.First(d => d.AttackType == attackType).GetInfo(_infoIndex);
        _weapon.Active(true);

        OnNext = false;
        OnProcess(info, frameCount).Forget();
    }

    public void RequestAt(AttackType attackType, int requestID, int frameCount = 0)
    {
        AttackInfomation info = _dataList.First(d => d.AttackType == attackType).GetInfo(requestID);
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

    /// <summary>
    /// �S�̃t���[���Ƃ̔�r�����Đ��ۂ�Ԃ�
    /// </summary>
    /// <param name="timer">�o�ߎ���</param>
    /// <param name="frameCount">�S�̂̃t���[����</param>
    /// <param name="checkFrame">�f�[�^����̃t���[����</param>
    bool Check(ref float timer, int frameCount, int checkFrame)
    {
        timer += Time.deltaTime;
        int currentFrame = (int)Mathf.Lerp(0, frameCount, timer);
        
        return currentFrame > checkFrame;
    }

    // ���L, IRetentionData
    public string RetentionPath => nameof(AttackController);

    public Object RetentionData()
    {
        return this;
    }
}
