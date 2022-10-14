using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// <summary>
/// カメラ制御クラス
/// </summary>

public partial class CameraController : MonoBehaviour
{
    [SerializeField] bool _isDebug;
    [SerializeField] Transform _user;
    [SerializeField] CinemachineFreeLook _freeLookCamera;
    
    InputOperator _inputOperator;

    public static CameraData Data { get; private set; }

    void Awake()
    {
        if (!enabled) return;

        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        gameObject.AddComponent<CinemachineBrain>();
        _freeLookCamera = Instantiate(_freeLookCamera);

        Data = new CameraData(_user);
    }

    void Start()
    {
        if (_isDebug)
        {
            SetUser(_user);
        }
    }

    void Update()
    {
        // ゲームパッドが接続されていれば動かす
        if (Gamepad.current != null)
        {
            Vector2 dir = _inputOperator.Player.Look.ReadValue<Vector2>().normalized;
            Move(dir);
        }

        Data.SetDir(this);
    }

    void OnDestroy()
    {
        Destroy(gameObject.GetComponent<CinemachineBrain>());
        Data = null;
    }

    // 使用者の設定
    public void SetUser(Transform user)
    {
        _freeLookCamera.Follow = user;
        _freeLookCamera.LookAt = user;

        _freeLookCamera.transform.SetParent(transform);
    }

    /// <summary>
    /// 使用者を基準に動かす
    /// </summary>
    /// <param name="dir">方向</param>
    void Move(Vector2 dir)
    {
        _freeLookCamera.m_XAxis.Value = dir.x;
        _freeLookCamera.m_YAxis.Value = dir.y;
    }
}