using UnityEngine;
using BehaviourTree;
using MonoState;

[RequireComponent(typeof(BehaviourTreeUser))]
public class Enemy : CharacterBase, IDamageble
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    }

    [SerializeField] int _hp;
    [SerializeField] BulletOperator _bulletOperator;

    Vector3 _beforePos;

    MonoStateMachine<Enemy> _stateMachine;
    EnemyRetentionData _retentionData;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Enemy>();
        _stateMachine.Initalize(this);

        _retentionData = gameObject.AddComponent<EnemyRetentionData>();
    }

    
    protected override void Setup()
    {
        _stateMachine
            .SetData(_bulletOperator)
            .SetData(Anim)
            .SetData(_retentionData);

        _stateMachine
            .AddState(new EnemyStateIdle(), State.Idle)
            .AddState(new EnemyStateMove(), State.Move)
            .AddState(new EnemyStateAttack(), State.Attack)
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

    // ‰º‹L, IDamageble
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Anim.SetRootMotion(true);
            Anim
                .AttaributeCallBack(() => Destroy(gameObject))
                .PlayRequest("Dead", AnimOperator.PlayType.Fade, AnimDuration);
        }
    }
}
