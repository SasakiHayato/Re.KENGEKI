using UnityEngine;

public interface IDamageble
{
    void GetDamage(int damage);
}

[RequireComponent(typeof(Rigidbody))]
public abstract class ChatracterBase : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] AnimOperator _animOperator;

    protected Rigidbody Rigidbody { get; private set; }

    readonly protected float Gravity = Physics.gravity.y;
    public static readonly float AnimDuration = 0.1f;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Setup();
    }

    protected abstract void Setup();

    protected float MoveSpeed => _moveSpeed;
    protected AnimOperator Anim => _animOperator;
}
