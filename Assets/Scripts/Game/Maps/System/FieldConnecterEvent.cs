using UnityEngine;
using System;

public class FieldConnecterEvent : MonoBehaviour
{
    int _id;
    Action<int, Transform, IFieldEventHandler> _action;

    public void SetData(Action<int, Transform, IFieldEventHandler> action, int id)
    {
        _id = id;
        _action = action;
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
