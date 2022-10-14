using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// <summary>
/// �J��������N���X
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
        // �Q�[���p�b�h���ڑ�����Ă���Γ�����
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

    // �g�p�҂̐ݒ�
    public void SetUser(Transform user)
    {
        _freeLookCamera.Follow = user;
        _freeLookCamera.LookAt = user;

        _freeLookCamera.transform.SetParent(transform);
    }

    /// <summary>
    /// �g�p�҂���ɓ�����
    /// </summary>
    /// <param name="dir">����</param>
    void Move(Vector2 dir)
    {
        _freeLookCamera.m_XAxis.Value = dir.x;
        _freeLookCamera.m_YAxis.Value = dir.y;
    }
}