using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _power;

    Action _action;
    Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

        ColliderEnable(false);
        Active(false);
    }

    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void ColliderEnable(bool isEnable)
    {
        _collider.enabled = isEnable;
    }

    public void SetHitAction(Action action)
    {
        _action = action;
    }

    void OnTriggerEnter(Collider other)
    {
        IDamageble damageble = other.GetComponent<IDamageble>();

        if (damageble != null)
        {
            _action?.Invoke();
            damageble.GetDamage(_power);
        }
    }
}
