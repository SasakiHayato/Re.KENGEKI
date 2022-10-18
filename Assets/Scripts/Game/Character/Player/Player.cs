using UnityEngine;
using MonoState;

public interface IDodgeEvent
{
    bool ExecutionDodgeEvent { get; }
    void ExecuteDodgeEvent();
}

/// <summary>
/// プレイヤーの管理クラス
/// </summary>

public class Player : CharacterBase, IFieldEventHandler, IDamageble, IDodgeEvent
{
    public enum State
    {
        Idle,
        Move,
        Dodge,
        Attack,
    }

    [SerializeField] AttackController _attackController;
    [SerializeField] GroundChecker _groundChecker;

    InputOperator _inputOperator;

    MonoStateMachine<Player> _stateMachine;
    PlayerRetentionData _retentionData;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Player>();
        _stateMachine.Initalize(this);

        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        _retentionData = gameObject.AddComponent<PlayerRetentionData>();
    }

    protected override void Setup()
    {
        if (CameraController.Infomation != null)
        {
            CameraController.Infomation.User = transform;
        }

        // 保持データの追加
        _stateMachine
            .SetData(_retentionData)
            .SetData(_attackController)
            .SetData(Anim);

        //  ステートの追加
        _stateMachine
            .AddState(new PlayerStateIdle(), State.Idle)
            .AddState(new PlayerStateMove(), State.Move)
            .AddState(new PlayerStateDodge(), State.Dodge)
            .AddState(new PlayerStateAttack(), State.Attack)
            .SetRunRequest(State.Idle);

        // 入力データの追加
        _inputOperator.Player.Dodge.performed += contextMenu => _retentionData.OnDodge = true;
        _inputOperator.Player.Attack.performed += contextMenu => OnAttack();

        // UI情報のセット
        UIPresenter presenter = GameManager.Instance.GetManager<UIPresenter>(nameof(UIPresenter));
        if (presenter != null)
        {
            presenter.ModelUpdate(WindowType.Game, "HP", new object[] { HP });
            presenter.ViewUpdate(WindowType.Game, "HP", new object[] { HP });
        }
    }

    void Update()
    {
        if (FieldEventExecution || GameManager.Instance.InputType != GameInputType.Player) return;

        if (!_retentionData.OnDodge)
        {
            Vector2 inputDir = _inputOperator.Player.Move.ReadValue<Vector2>();
            _retentionData.SetInputDir(inputDir);
        }

        if (_groundChecker.IsGround)
        {
            Move();
        }
        else
        {
            Rigidbody.velocity = new Vector3(0, Gravity, 0);
        }
        
        Rotate();
    }

    void OnDestroy()
    {
        _inputOperator.Disable();
        Destroy(GetComponent<PlayerRetentionData>());
    }

    void Move()
    {
        Vector2 dir = _retentionData.ReadInputDir;
        Vector3 move;

        if (CameraController.Infomation != null)
        {
            Vector3 forward = CameraController.Infomation.PlaneFoward * dir.y;
            Vector3 right = CameraController.Infomation.PlaneRight * dir.x;

            move = (forward + right).normalized * MoveSpeed;
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
        Vector2 dir = _retentionData.ReadInputDir;
        Vector3 rotate;

        if (CameraController.Infomation != null)
        {
            Vector3 forward = CameraController.Infomation.PlaneFoward * dir.y;
            Vector3 right = CameraController.Infomation.PlaneRight * dir.x;

            rotate = new Vector3(forward.x + right.x, 0, forward.z + right.z).normalized;
        }
        else
        {
            rotate = new Vector3(dir.x, 0, dir.y);
        }

        if (rotate.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(rotate);
        }
    }

    void OnAttack()
    {
        if (_retentionData.OnAttack && _attackController.OnNext)
        {
            _retentionData.OnNextAttack = true;
        }
        else
        {
            _retentionData.OnNextAttack = false;
            _retentionData.OnAttack = true;
        }
    }

    public void OnCounter(int counterID)
    {
        _retentionData.CounterID = counterID;
        _retentionData.OnAttack = true;
    }

    // 下記, IDamageble
    public void GetDamage(int damage)
    {
        if (_retentionData.OnDodge)
        {
            return;
        }
    }

    // 下記, IDodgeEvent
    public void ExecuteDodgeEvent()
    {
        if (!_retentionData.OnDodge || ExecutionDodgeEvent) return;

        ExecutionDodgeEvent = true;

        // エフェクトイベントの登録
        EffectEventData eventData = new EffectEventData();
        eventData.EffectEvent = new SlowTimeEffect();
        // 引数1. TimeScale; 引数2. Timer;
        eventData.Values = new object[] { 0.5f, 0.25f };
        
        EffectOperator effectOperator = GameManager.Instance.GetManager<EffectOperator>(nameof(EffectOperator));
        effectOperator.Request(eventData);

        CameraController camera = GameManager.Instance.GetManager<CameraController>(nameof(CameraController));
        camera.TransitionEventCm
            (
                "Player", 
                0.1f, 
                () => GameManager.Instance
                .GetManager<UIInputOperator>(nameof(UIInputOperator))
                .RequestOperation
                    (
                        new UIInputCounter(() => 
                        {
                            ExecutionDodgeEvent = false;
                            _retentionData.OnDodge = false;
                        }),
                        () => _stateMachine.CurrentStatePath == State.Dodge.ToString()
                    )
            );
    }

    public bool ExecutionDodgeEvent { get; private set; }

    // 下記, IFieldEventHandler
    public bool FieldEventExecution { private get; set; }
}
