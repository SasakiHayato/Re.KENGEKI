using UnityEngine;
using System.Linq;
using DG.Tweening;

/// <summary>
/// カメラ制御クラス
/// </summary>

public partial class CameraController : MonoBehaviour, IManager
{
    [SerializeField] Transform _user;
    [SerializeField] CameraData _data;
    [SerializeField] CameraTaregtData _taregtData;

    Camera _camera;
    InputOperator _inputOperator;
    EventCamera _eventCamera;

    float _lockAtTimer;

    float _horizontalAngle;
    float _verticleAngle;

    bool _isEvent;
    
    public static CameraInfomation Infomation { get; private set; }

    public static Skybox Skybox { get; private set; }

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

        GameManager.Instance.AddManager(this);

        Skybox = GetComponent<Skybox>();
    }

    void Start()
    {
        _inputOperator.Player.LockOn.performed += contextMenu => SetLockOn();
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (_eventCamera == null)
        {
            Move();
            LockAt();
        }
        
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
            if (Infomation.AxisHrosontal == CmInputType.Standard)
            {
                _horizontalAngle -= _data.MoveSpeed.x * value;
            }
            else
            {
                _horizontalAngle += _data.MoveSpeed.x * value;
            }
            
        }

        float rad = _horizontalAngle * Mathf.Deg2Rad;
        
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    float AxisY(float value)
    {
        if (value != 0 && _data.Sencivity.y <= Mathf.Abs(value))
        {
            if (Infomation.AxisVerticle == CmInputType.Standard)
            {
                _verticleAngle += _data.MoveSpeed.y * value;
            }
            else
            {
                _verticleAngle -= _data.MoveSpeed.y * value;
            }
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
    void LockAt()
    {
        Vector3 offset;
        _lockAtTimer += Time.deltaTime * _data.DumpingSpeed;

        if (_taregtData.Target != null)
        {
            offset = _taregtData.Target.position + _data.OffsetView.normalized;
        }
        else
        {
            offset = Infomation.User.position + _data.OffsetView.normalized;
        }

        Vector3 forward = (offset - transform.position).normalized;

        Quaternion rotate = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), _lockAtTimer);
        transform.rotation = rotate;

        if (rotate == transform.rotation)
        {
            _lockAtTimer = 0;
        }
    }

    void SetLockOn()
    {
        if (_taregtData.Target != null || _eventCamera != null)
        {
            _taregtData.Target = null;
            return;
        }
        
        try
        {
            float dist = _taregtData.AttaributeDistance;

            // Targetの取得
            Transform target = Infomation.CmTargetList
            .Where(c => c.TargetRenderer.isVisible)
            .Where(c => Vector3.Distance(c.Target().position, transform.position) < dist)
            .OrderBy(c => Vector3.Dot(transform.forward, transform.position - c.Target().position))
            .FirstOrDefault()
            .Target();

            _taregtData.Target = target;
        }
        catch
        {
            _taregtData.Target = null;
        }
    }

    public void TransitionEventCm(string path, float duration = 0.2f, System.Action action = null)
    {
        EventCamera camera = Infomation.GetEventCamera(path);

        transform
            .DOMove(camera.transform.position, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _camera.enabled = false;
                camera.IsView(true);
                _eventCamera = camera;
                action?.Invoke();
            });
    }

    public void CallBackTransition(float duration = 0.2f)
    {
        if (_eventCamera == null) 
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
            .OnComplete(() => 
            {
                _eventCamera.IsView(false);
                _camera.enabled = true;
                _eventCamera = null;
            });
    }

    // 下記, IManager
    public string Key => nameof(CameraController);
    public Object Type()
    {
        return this;
    }
}