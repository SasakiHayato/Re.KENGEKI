using UnityEngine;
using MonoState;
using MonoState.Data;

/// <summary>
/// プレイヤーの管理クラス
/// </summary>

public class Player : MonoBehaviour, IRetentionData
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    }

    [SerializeField] AnimOperator _animOperator;
    [SerializeField] float _moveSpeed = 1;

    // Note. 回転の為に使用
    Vector3 _beforePos;

    Rigidbody _rb;
    InputOperator _inputOperator;

    MonoStateMachine<Player> _stateMachine;

    /// <summary>
    /// trueなら行動中
    /// </summary>
    public bool OnMove
    {
        get
        {
            Vector2 dir = _inputOperator.Player.Move.ReadValue<Vector2>();

            if (Mathf.Abs(dir.x) <= 0 && Mathf.Abs(dir.y) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    readonly float Gravity = Physics.gravity.y;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Player>();
        _stateMachine.Initalize(this);

        _inputOperator = new InputOperator();
        _inputOperator.Enable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _beforePos = transform.position;

        // 保持データの追加
        _stateMachine
            .SetData(this)
            .SetData(_animOperator);

        //  ステートの追加
        _stateMachine
            .AddState(new Idle(), State.Idle)
            .AddState(new Move(), State.Move)
            .SetRunRequest(State.Idle);
    }

    void Update()
    {
        Move(_inputOperator.Player.Move.ReadValue<Vector2>());
        Rotate();
    }

    void OnDestroy()
    {
        _inputOperator.Disable();
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

    // 下記, IRetentionData
    public string RetentionPath => nameof(Player);

    public Object RetentionData()
    {
        return this;
    }
}
