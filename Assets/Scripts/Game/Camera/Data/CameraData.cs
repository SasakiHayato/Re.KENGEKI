using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraData
{
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] Vector3 _offsetView;
    [SerializeField] float _distance;
    [SerializeField] float _limitLowAngle;
    [SerializeField] float _limitHeightAngle;
    [SerializeField] Vector2 _moveSpeed;
    [SerializeField] Vector2 _sencivity;

    public Vector3 OffsetPosition => _offsetPosition;
    public Vector3 OffsetView => _offsetView;
    public float Distance => _distance;
    public float LimitHeghtAngle => _limitHeightAngle;
    public float LimitLowAngle => _limitLowAngle;
    public Vector2 MoveSpeed => _moveSpeed;
    public Vector2 Sencivity => _sencivity;
}
