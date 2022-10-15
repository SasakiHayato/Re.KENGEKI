using UnityEngine;

public class SetParticleEffect : IEffectEvent
{
    public string Path => nameof(SetParticleEffect);
    public object[] SendValues { private get; set; }
    public Transform User { private get; set; }

    public void OnEnable()
    {
        string path = EffectOperator.ParticlePath + (string)SendValues[0];
        GameObject particle = Object.Instantiate(Resources.Load<GameObject>(path));

        particle.transform.position = (Vector3)SendValues[1];
    }

    public bool Execute()
    {
        return true;
    }

    public void Initalize()
    {
        
    }
}
