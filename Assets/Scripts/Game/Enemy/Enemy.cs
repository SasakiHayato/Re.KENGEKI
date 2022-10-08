using UnityEngine;
using BehaviourTree;
using MonoState;

[RequireComponent(typeof(BehaviourTreeUser))]
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
    }

    [SerializeField] float _moveSpeed = 1;
    [SerializeField] AnimOperator _animOperator;

    Rigidbody _rb;

    MonoStateMachine<Enemy> _stateMachine;
    EnemyRetentionData _retentionData;

    readonly float Gravity = Physics.gravity.y;
    public static readonly float AnimDuration = 0.1f;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Enemy>();
        _stateMachine.Initalize(this);

        _retentionData = gameObject.AddComponent<EnemyRetentionData>();
    }

    void Start()
    {
        _stateMachine
            .SetData(_animOperator)
            .SetData(_retentionData);

        _stateMachine
            .AddState(new EnemyStateIdle(), State.Idle)
            .AddState(new EnemyStateMove(), State.Move)
            .SetRunRequest(State.Idle);

        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = _retentionData.MoveDir * _moveSpeed;
        move.y = Gravity;

        _rb.velocity = move;
    }

    void OnDestroy()
    {
        Destroy(GetComponent<EnemyRetentionData>());
    }
}
