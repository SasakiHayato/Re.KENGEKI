using UnityEngine;

public interface IDamageble
{
    void GetDamage(int damage);
}

[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] int _hp = 10;
    [SerializeField] AnimOperator _animOperator;

    protected Rigidbody Rigidbody { get; private set; }

    readonly protected float Gravity = Physics.gravity.y;
    public static readonly float AnimDuration = 0.1f;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        HP = _hp;
        Setup();
    }

    protected abstract void Setup();

    protected float MoveSpeed => _moveSpeed;
    protected int HP { get; set; }
    protected AnimOperator Anim => _animOperator;
}
