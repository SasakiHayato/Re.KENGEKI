using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmTarget
{
    Renderer TargetRenderer { get; }
    Transform Target();
}

[System.Serializable]
public class CameraTaregtData
{
    [SerializeField] float _attributeDistance;
    [SerializeField] Vector3 _offsetPosition;

    public Transform Target { get; set; }

    public float AttaributeDistance => _attributeDistance;
    public Vector3 OffsetPosition => _offsetPosition;
}
