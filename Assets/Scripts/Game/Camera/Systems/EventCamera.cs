using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : MonoBehaviour
{
    [SerializeField] string _path;

    public string Path => _path;

    void Start()
    {
        CameraController.Infomation.AddEventCamera(this);
    }
}
