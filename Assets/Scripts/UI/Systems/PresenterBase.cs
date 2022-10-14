using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PresenterBase : MonoBehaviour
{
    [SerializeField] string _path;

    public string Path => _path;

    void Awake()
    {
        Setup();
    }

    protected abstract void Setup();

    public abstract void CallBackView(object[] values);

    public abstract void CallBackModel(object[] values);
}
