using System;
using System.Collections.Generic;
using UnityEngine;

public struct EffectEventData
{
    public IEffectEvent EffectEvent { get; set; }
    public object[] Values { get; set; }
    public Transform User { get; set; }

    public Action CallBack { get; set; }
}
