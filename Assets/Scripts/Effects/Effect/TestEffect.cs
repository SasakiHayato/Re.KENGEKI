using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : IEffectEvent
{
    public string Path => nameof(TestEffect);

    public Transform User { private get; set; }

    public void Setup()
    {
        
    }

    public bool Execute()
    {
        Debug.Log("Execute TestEffect");

        return true;
    }

    public void Initalize()
    {
        
    }
}
