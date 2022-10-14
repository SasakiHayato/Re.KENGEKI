using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _power;
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

    void OnTriggerEnter(Collider other)
    {
        IDamageble damageble = other.GetComponent<IDamageble>();

        if (damageble != null)
        {
            damageble.GetDamage(_power);
        }
    }
}
