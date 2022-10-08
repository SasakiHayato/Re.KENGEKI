using UnityEngine;
using ObjectPool;
using MonoState.Data;
using System;
using Cysharp.Threading.Tasks;

public class BulletOperator : MonoBehaviour, IRetentionData
{
    public struct SendData
    {
        public float MoveSpeed;
        public float AttackPower;
        public BulletData.Model Model;
    }

    [SerializeField] Transform _mozzle;
    [SerializeField] Bullet _bullet;
    [SerializeField] int _createCount = 5;
    [SerializeField] BulletData _bulletData;

    public bool IsExecution { get; private set; } = false;

    Pool<Bullet> _bulletPool = new Pool<Bullet>();

    public static readonly float BulletActiveTime = 5f;

    void Awake()
    {
        if (_mozzle == null)
        {
            _mozzle = transform;
        }

        _bulletPool
            .SetMono(_bullet, _createCount)
            .IsSetParent(transform)
            .CreateRequest();
    }

    public void ShotRequest()
    {
        if (IsExecution) return;

        IsExecution = true;
        Shot().Forget();
    }

    async UniTask Shot()
    {
        for (int index = 0; index < _bulletData.ModelList.Count; index++)
        {
            Debug.Log("aaa");

            Action action;
            Bullet bullet = _bulletPool.UseRequest(out action);

            SetData(bullet, index);

            await UniTask.Delay(TimeSpan.FromSeconds(_bulletData.DelayTime));
            
            action.Invoke();
        }

        IsExecution = false;
    }

    void SetData(Bullet bullet, int index)
    {
        SendData sendData = new SendData();
        sendData.MoveSpeed = _bulletData.MoveSpeed;
        sendData.AttackPower = _bulletData.AttackPower;
        sendData.Model = _bulletData.ModelList[index];

        bullet.SetData(sendData, _mozzle);
    }

    // ‰º‹L, IRetentionData
    public string RetentionPath => nameof(BulletOperator);

    public UnityEngine.Object RetentionData()
    {
        return this;
    }
}
