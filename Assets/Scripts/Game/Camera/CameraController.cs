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

    public static CameraInfomation Data { get; private set; }

    void Awake()
    {
        if (!enabled) return;

        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        gameObject.AddComponent<CinemachineBrain>();
        _freeLookCamera = Instantiate(_freeLookCamera);

        Data = new CameraInfomation(SetUser);
        Data.User = _user;
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
    void SetUser(Transform user)
    {
        _freeLookCamera.Follow = user;
        _freeLookCamera.LookAt = user;

        for (int index = 0; index < 3; index++)
        {
            _freeLookCamera.GetRig(index).LookAt = user;
        }

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