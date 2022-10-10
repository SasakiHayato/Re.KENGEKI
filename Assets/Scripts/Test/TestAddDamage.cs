using UnityEngine;

public class TestAddDamage : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] int _damage;

    IDamageble _damageble;

    void Start()
    {
        _damageble = _target.GetComponent<IDamageble>();
    }

    public void AddDamage()
    {
        _damageble.GetDamage(_damage);
    }
}
