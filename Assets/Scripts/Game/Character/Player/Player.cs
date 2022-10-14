using UnityEngine;
using MonoState;

public interface IDodgeEvent
{
    bool ExecutionDodgeEvent { get; }
    void ExecuteDodgeEvent();
}

/// <summary>
/// �v���C���[�̊Ǘ��N���X
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

    // Note. ��]�ׂ̈Ɏg�p
    Vector3 _beforePos;

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
        _beforePos = transform.position;

        // �ێ��f�[�^�̒ǉ�
        _stateMachine
            .SetData(_retentionData)
            .SetData(_attackController)
            .SetData(Anim);

        //  �X�e�[�g�̒ǉ�
        _stateMachine
            .AddState(new PlayerStateIdle(), State.Idle)
            .AddState(new PlayerStateMove(), State.Move)
            .AddState(new PlayerStateDodge(), State.Dodge)
            .AddState(new PlayerStateAttack(), State.Attack)
            .SetRunRequest(State.Idle);

        // ���̓f�[�^�̒ǉ�
        _inputOperator.Player.Dodge.performed += contextMenu => _retentionData.OnDodge = true;
        _inputOperator.Player.Attack.performed += contextMenu => _retentionData.OnAttack = true;
    }

    void Update()
    {
        if (FieldEventExecution) return;

        if (!_retentionData.OnDodge)
        {
            Vector2 inputDir = _inputOperator.Player.Move.ReadValue<Vector2>();
            _retentionData.SetInputDir(inputDir);
        }

        Move();
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

    // ���L, IDamageble
    public void GetDamage(int damage)
    {
        if (_retentionData.OnDodge)
        {
            return;
        }
    }

    // ���L, IDodgeEvent
    public void ExecuteDodgeEvent()
    {
        if (!_retentionData.OnDodge || ExecutionDodgeEvent) return;

        ExecutionDodgeEvent = true;

        // �G�t�F�N�g�C�x���g�̓o�^
        EffectEventData eventData = new EffectEventData();
        eventData.EffectEvent = new SlowTimeEffect();
        eventData.Values = new object[] { 0.5f, 0.5f };
        eventData.CallBack = () => { ExecutionDodgeEvent = false; };

        EffectOperator effectOperator = GameManager.Instance.GetManager<EffectOperator>(nameof(EffectOperator));
        effectOperator.Request(eventData);
    }

    public bool ExecutionDodgeEvent { get; private set; }

    // ���L, IFieldEventHandler
    public bool FieldEventExecution { private get; set; }
}
