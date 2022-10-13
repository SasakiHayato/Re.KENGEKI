using UnityEngine;
using System.Collections;

public interface IEffectEvent
{
    string Path { get; }
    object[] SendValues { set; }
    Transform User { set; }
    void OnEnable();
    bool Execute();
    void Initalize();
}

public class EffectOperator : MonoBehaviour, IManager
{
    public static string ParticlePath => "Assets/Resource/Effects/";

    void Awake()
    {
        GameManager.Instance.AddManager(this);
    }

    public void Request(EffectEventData eventData)
    {
        IEffectEvent effectEvent = eventData.EffectEvent;
        effectEvent.SendValues = eventData.Values;
        effectEvent.User = eventData.User;

        try
        {
            StartCoroutine(Execute(effectEvent, eventData.CallBack));
        }
        catch
        {
            Debug.Log($"Effect発動時にエラーが出ました。EffectPath {effectEvent.Path}");
        }
    }

    IEnumerator Execute(IEffectEvent effectEvent, System.Action action)
    {
        effectEvent.OnEnable();

        while (!effectEvent.Execute())
        {
            yield return null;
        }

        effectEvent.Initalize();
        action?.Invoke();
    }

    // IManager
    public string Key => nameof(EffectOperator);
    public Object Type()
    {
        return this;
    }
}
