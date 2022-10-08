using UnityEngine;
using ObjectPool;

public class Bullet : MonoBehaviour, IPool, IPoolEvent
{
    float _moveSpeed;
    int _attackPower;
    float _activeTimer;
    bool _isHoming;

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

        if (sendData.Model.ShotType == BulletData.ShotType.Homing)
        {
            _isHoming = true;
        }
    }

    public void OnEnableEvent()
    {
        gameObject.SetActive(true);
        gameObject.transform.SetParent(null);

        Vector3 muzzle;

        if (_model.Point == null)
        {
            muzzle = Vector3.zero;
        }
        else
        {
            muzzle = _model.Point.position;
        }

        Vector3 offset = _muzzle.position + muzzle;

        if (_model.ShotType == BulletData.ShotType.ToPlayer)
        {
            gameObject.transform.position = offset;
            _dir = GameManager.Instance.GameUser.position - offset;
        }
        else
        {
            gameObject.transform.position = offset;
        }

        _activeTimer = 0;
    }

    public bool Execute()
    {
        _activeTimer += Time.deltaTime;

        if (_isHoming)
        {
            _dir = GameManager.Instance.GameUser.position - transform.position;
        }

        _rb.velocity = _dir.normalized * _moveSpeed;

        return _activeTimer > BulletOperator.BulletActiveTime;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageble damageble = other.GetComponent<IDamageble>();
        
        if (damageble != null)
        {
            damageble.GetDamage(_attackPower);
            IsDone = true;
        }
    }
}
