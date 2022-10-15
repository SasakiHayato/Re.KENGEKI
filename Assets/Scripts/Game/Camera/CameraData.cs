using UnityEngine;

/// <summary>
/// カメラのデータクラス
/// </summary>

public class CameraData
{
    public CameraData(Transform user)
    {
        User = user;
    }

    public Vector3 Foward { get; private set; }
    public Vector3 PlaneFoward { get; private set; }

    public Vector3 Right { get; private set; }

    public Vector3 PlaneRight { get; private set; }

    public Transform User { get; private set; }

    public void SetDir(CameraController camera)
    {
        Foward = camera.transform.forward;
        Right = camera.transform.right;

        PlaneFoward = new Vector3(Foward.x, 0, Foward.z).normalized;
        PlaneRight = new Vector3(Right.x, 0, Right.z).normalized;
    }
}
