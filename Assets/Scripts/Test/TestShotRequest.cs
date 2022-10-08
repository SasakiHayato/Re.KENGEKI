using UnityEngine;

public class TestShotRequest : MonoBehaviour
{
    [SerializeField] float _intervalTime;
    [SerializeField] BulletOperator _bulletOperator;

    float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _intervalTime)
        {
            _timer = 0;
            Request();
        }
    }

    public void Request()
    {
        _bulletOperator.ShotRequest();
    }
}
