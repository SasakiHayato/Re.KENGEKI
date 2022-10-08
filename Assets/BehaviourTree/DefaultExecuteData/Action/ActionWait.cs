using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// w’èŠÔ‘Ò‚½‚¹‚éAIs“®
/// </summary>
public class ActionWait : BehaviourAction
{
    [SerializeField] float _waitTime;

    float _timer;
    protected override bool Execute()
    {
        _timer += Time.deltaTime;

        Debug.Log(_timer > _waitTime);

        return _timer > _waitTime;
    }

    protected override void Initialize()
    {
        _timer = 0;
    }
}
