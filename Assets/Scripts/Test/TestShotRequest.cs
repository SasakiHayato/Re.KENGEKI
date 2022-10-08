using UnityEngine;

public class TestShotRequest : MonoBehaviour
{
    [SerializeField] BulletOperator _bulletOperator;

    public void Request()
    {
        _bulletOperator.ShotRequest();
    }
}
