using UnityEngine;

public class SlowTimeEffect : IEffectEvent
{
    float _timer;

    public string Path => nameof(SlowTimeEffect);
    public Transform User { private get; set; }
    public object[] SendValues { private get; set; }

    public void OnEnable()
    {
        float rate = (float)SendValues[0];
        _timer = (float)SendValues[1];

        Time.timeScale = rate;
    }

    public bool Execute()
    {
        _timer -= Time.deltaTime;

        return _timer <= 0;
    }

    public void Initalize()
    {
        Time.timeScale = 1;
    }
}
