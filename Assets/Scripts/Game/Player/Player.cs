using UnityEngine;
using MonoState;

/// <summary>
/// プレイヤーの管理クラス
/// </summary>

public class Player : MonoBehaviour, IFieldEventHandler
{
    public enum State
    {
        Idle,
        Move,
        Dodge,
    }

    [SerializeField] AnimOperator _animOperator;
    [SerializeField] float _moveSpeed = 1;

    // Note. 回転の為に使用
    Vector3 _beforePos;

    Rigidbody _rb;
    InputOperator _inputOperator;

    MonoStateMachine<Player> _stateMachine;
    PlayerRetentionData _playerData;

    readonly float Gravity = Physics.gravity.y;
    public static readonly float AnimDuration = 0.1f;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Player>();
        _stateMachine.Initalize(this);

        _inputOperator = new InputOperator();
        _inputOperator.Enable();
    }

    void Start()
    {
        _playerData = gameObject.AddComponent<PlayerRetentionData>();

        _rb = GetComponent<Rigidbody>();
        _beforePos = transform.position;

        // 保持データの追加
        _stateMachine
            .SetData(_playerData)
            .SetData(_animOperator);

        //  ステートの追加
        _stateMachine
            .AddState(new PlayerStateIdle(), State.Idle)
            .AddState(new PlayerStateMove(), State.Move)
            .AddState(new PlayerStateDodge(), State.Dodge)
            .SetRunRequest(State.Idle);

        // 入力データの追加
        _inputOperator.Player.Dodge.performed += contextMenu => _playerData.OnDodge = true;
    }

    void Update()
    {
        if (FieldEventExecution) return;

        Vector2 inputDir = _inputOperator.Player.Move.ReadValue<Vector2>();
        _playerData.SetInputDir(inputDir);

        Move(inputDir);
        Rotate();
    }

    void OnDestroy()
    {
        _inputOperator.Disable();
        Destroy(GetComponent<PlayerRetentionData>());
    }

    void Move(Vector2 dir)
    {
        Vector3 move;

        if (CameraController.Data != null)
        {
            Vector3 forward = CameraController.Data.Foward * dir.y;
            Vector3 right = CameraController.Data.Right * dir.x;

            move = (forward + right) * _moveSpeed;
        }
        else
        {
            move = new Vector3(dir.x, 0, dir.y) * _moveSpeed;
        }

        move.y = Gravity;
        _rb.velocity = move;
    }

    void Rotate()
    {
        Vector3 diff = transform.position - _beforePos;
        diff.y = 0;

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }

        _beforePos = transform.position;
    }

    // 下記, IFieldEventHandler
    public bool FieldEventExecution { private get; set; }
}
