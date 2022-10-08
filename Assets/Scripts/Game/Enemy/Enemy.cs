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

    [SerializeField] AnimOperator _animOperator;

    MonoStateMachine<Enemy> _stateMachine;

    public static readonly float AnimDuration = 0.1f;

    void Awake()
    {
        _stateMachine = new MonoStateMachine<Enemy>();
        _stateMachine.Initalize(this);
    }

    void Start()
    {
        _stateMachine.SetData(_animOperator);

        _stateMachine
            .AddState(new EnemyStateIdle(), State.Idle)
            .AddState(new EnemyStateMove(), State.Move)
            .SetRunRequest(State.Idle);
    }
}
