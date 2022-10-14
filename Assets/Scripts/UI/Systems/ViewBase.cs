using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField] string _path;

    public string Path => _path;

    void Awake()
    {
        Setup();
    }

    protected abstract void Setup();

    public abstract void CallBack(object[] values);
}
