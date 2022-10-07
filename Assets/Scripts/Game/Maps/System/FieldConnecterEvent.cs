using UnityEngine;
using System;

public class FieldConnecterEvent : MonoBehaviour
{
    int _id;

    Collider _collider;
    Action<int, Transform, IFieldEventHandler> _action;

    public void SetData(Action<int, Transform, IFieldEventHandler> action, int id)
    {
        _id = id;
        _collider = GetComponent<Collider>();
        _action = action;
    }

    public void ColliderActive(bool active)
    {
        _collider.enabled = active;
    }

    void OnTriggerEnter(Collider other)
    {
        IFieldEventHandler handler = other.GetComponent<IFieldEventHandler>();

        if (handler != null)
        {
            _action.Invoke(_id, other.transform, handler);
        }
    }
}
