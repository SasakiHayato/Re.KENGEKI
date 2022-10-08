using UnityEngine;
using ObjectPool;

public class Bullet : MonoBehaviour, IPool, IPoolEvent
{
    float _moveSpeed;
    float _attackPower;
    float _activeTimer;

    Vector3 _dir;

    Rigidbody _rb;
    Transform _muzzle;
    BulletData.Model _model;

    public bool IsDone { get; set; }

    public void Setup(Transform parent)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        gameObject.SetActive(false);
    }

    public void SetData(BulletOperator.SendData sendData, Transform muzzle)
    {
        _moveSpeed = sendData.MoveSpeed;
        _attackPower = sendData.AttackPower;
        _model = sendData.Model;
        _muzzle = muzzle;
    }

    public void OnEnableEvent()
    {
        gameObject.SetActive(true);
        gameObject.transform.SetParent(null);

        if (_model.ShotType == BulletData.ShotType.ToPlayer)
        {
            Vector3 muzzle;

            if (_model.Point == null)
            {
                muzzle = _muzzle.position;
            }
            else
            {
                muzzle = _model.Point.position;
            }

            Vector3 offset = _muzzle.position + muzzle;
            gameObject.transform.position = offset;

            _dir = GameManager.Instance.GameUser.position - offset;
        }

        _activeTimer = 0;
    }

    public bool Execute()
    {
        _activeTimer += Time.deltaTime;

        _rb.velocity = _dir * _moveSpeed;

        return _activeTimer > BulletOperator.BulletActiveTime;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IsDone = true;
    }
}
