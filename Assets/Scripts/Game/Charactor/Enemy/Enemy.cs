using UnityEngine;
using BehaviourTree;
using MonoState;

[RequireComponent(typeof(BehaviourTreeUser))]
public class Enemy : ChatractorBase
{
    public enum State
    {
        Idle,
        Move,
    }

    Vector3 _beforePos;

    MonoStateMachine<Enemy> _stateMachine;
    EnemyRetentionData _retentionData;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Enemy>();
        _stateMachine.Initalize(this);

        _retentionData = gameObject.AddComponent<EnemyRetentionData>();
    }

    void Start()
    {
        _stateMachine
            .SetData(AnimOperator)
            .SetData(_retentionData);

        _stateMachine
            .AddState(new EnemyStateIdle(), State.Idle)
            .AddState(new EnemyStateMove(), State.Move)
            .SetRunRequest(State.Idle);

        _beforePos = transform.position;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 move = _retentionData.MoveDir * MoveSpeed;
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

    void OnDestroy()
    {
        Destroy(GetComponent<EnemyRetentionData>());
    }
}
