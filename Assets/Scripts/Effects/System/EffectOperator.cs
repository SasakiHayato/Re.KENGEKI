using UnityEngine;
using System.Collections;

public interface IEffectEvent
{
    string Path { get; }
    Transform User { set; }
    void Setup();
    bool Execute();
    void Initalize();
}

public class EffectOperator : MonoBehaviour
{
    public struct Event
    { 
        public static void SetData(IEffectEvent effectEvent, Transform user = null)
        {
            if (Instance == null)
            {
                Debug.Log("EffectOperatorのInstanceがありません");
                return;
            }

            Instance.SetEffectEvent(effectEvent, user);
        }
    }

    static EffectOperator Instance;

    public static string ParticlePath => "Assets/Resoruce/Effects/";

    void Awake()
    {
        Instance = this;
    }

    protected void SetEffectEvent(IEffectEvent effectEvent, Transform user = null)
    {
        try
        {
            effectEvent.User = user;
            StartCoroutine(Execute(effectEvent));
            
        }
        catch
        {
            Debug.Log($"Effect発動時にエラーが出ました。EffectPath {effectEvent.Path}");
        }
    }

    IEnumerator Execute(IEffectEvent effectEvent)
    {
        effectEvent.Setup();

        while (!effectEvent.Execute())
        {
            yield return null;
        }

        effectEvent.Initalize();
    }
}
