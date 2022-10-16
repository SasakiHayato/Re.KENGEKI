using UnityEngine;

public class TestAttributeCmTarget : MonoBehaviour, ICmTarget
{
    void Start()
    {
        TargetRenderer = GetComponent<Renderer>();
        CameraController.Infomation.AddITarget(this);
    }

    public Transform Target()
    {
        return transform;
    }

    public Renderer TargetRenderer { get; private set; }
}
