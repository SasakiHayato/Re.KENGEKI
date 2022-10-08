using UnityEngine;
using MonoState;

/// <summary>
/// プレイヤーの管理クラス
/// </summary>

public class Player : ChatractorBase, IFieldEventHandler
{
    public enum State
    {
        Idle,
        Move,
        Dodge,
    }

    // Note. 回転の為に使用
    Vector3 _beforePos;

    InputOperator _inputOperator;

    MonoStateMachine<Player> _stateMachine;
    PlayerRetentionData _playerData;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Player>();
        _stateMachine.Initalize(this);

        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        _playerData = gameObject.AddComponent<PlayerRetentionData>();
    }

    void Start()
    {
        _beforePos = transform.position;

        // 保持データの追加
        _stateMachine
            .SetData(_playerData)
            .SetData(AnimOperator);

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

            move = (forward + right) * MoveSpeed;
        }
        else
        {
            move = new Vector3(dir.x, 0, dir.y) * MoveSpeed;
        }

        move.y = Gravity;
        Rigidbody.velocity = move;
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
