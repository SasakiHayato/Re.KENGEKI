using UnityEngine;
using DG.Tweening;

/// <summary>
/// カメラ制御クラス
/// </summary>

public partial class CameraController : MonoBehaviour
{
    [SerializeField] Transform _user;
    [SerializeField] CameraData _data;
    
    InputOperator _inputOperator;

    float _horizontalAngle;
    float _verticleAngle;

    bool _isEvent;

    public static CameraInfomation Infomation { get; private set; }

    readonly float Coefficient = 0.5f;
    readonly float RateLimit = 0.2f;

    void Awake()
    {
        if (!enabled) return;

        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        Infomation = new CameraInfomation(SetUser);
        Infomation.User = _user;

        SetUser(_user);
    }

    void Update()
    {
        if (!_isEvent)
        {
            Move();
        }

        LookAt();

        Infomation.SetCameraDir(this);
    }

    void OnDestroy()
    {
        Infomation = null;
    }

    // 使用者の設定
    void SetUser(Transform user)
    {
        _user = user;
    }

    /// <summary>
    /// 使用者を基準に動かす
    /// </summary>
    void Move()
    {
        Vector2 dir = _inputOperator.Player.Look.ReadValue<Vector2>().normalized;

        Vector3 move = AxisX(dir.x);
        move.y = AxisY(dir.y);

        // 二次関数
        float rate = Mathf.Sqrt(Mathf.Abs(move.y)) * Coefficient;

        if (rate < RateLimit)
        {
            rate = RateLimit;
        }

        transform.position = (move * _data.Distance / rate) + (_user.position + _data.OffsetPosition);
    }

    Vector3 AxisX(float value)
    {
        if (value != 0 && _data.Sencivity.x <= Mathf.Abs(value))
        {
            _horizontalAngle += _data.MoveSpeed.x * value;
        }

        float rad = _horizontalAngle * Mathf.Deg2Rad;
        
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    float AxisY(float value)
    {
        if (value != 0 && _data.Sencivity.y <= Mathf.Abs(value))
        {
            _verticleAngle += _data.MoveSpeed.y * value;
        }

        if (_data.LimitHeghtAngle < _verticleAngle)
        {
            _verticleAngle = _data.LimitHeghtAngle;
        }

        if (_data.LimitLowAngle > _verticleAngle)
        {
            _verticleAngle = _data.LimitLowAngle;
        }

        float rad = _verticleAngle * Mathf.Deg2Rad;

        return Mathf.Sin(rad);
    }

    /// <summary>
    /// 使用者をとらえる
    /// </summary>
    void LookAt()
    {
        Vector3 offset = Infomation.User.position + _data.OffsetView.normalized;
        Vector3 forward = (offset - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(forward);
    }

    public void TransitionEventCm(string path, float duration = 0.2f)
    {
        EventCamera camera = Infomation.GetEventCamera(path);

        transform
            .DOMove(camera.transform.position, duration)
            .SetEase(Ease.Linear);

        _isEvent = true;
    }

    public void CallBackTransition(float duration = 0.2f)
    {
        if (!_isEvent) 
        {
            return;
        }

        Vector3 move = AxisX(0);
        move.y = AxisY(0);

        // 二次関数
        float rate = Mathf.Sqrt(Mathf.Abs(move.y)) * Coefficient;

        if (rate < RateLimit)
        {
            rate = RateLimit;
        }

        Vector3 position = (move * _data.Distance / rate) + (_user.position + _data.OffsetPosition);

        transform
            .DOMove(position, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isEvent = false);
    }
}