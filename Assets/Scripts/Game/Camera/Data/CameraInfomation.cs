using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// カメラのデータクラス
/// </summary>

public class CameraInfomation
{
    public CameraInfomation(Action<Transform> action)
    {
        _action = action;
        _eventCameraList = new List<EventCamera>();
    }

    Transform _user;
    Action<Transform> _action;
    List<EventCamera> _eventCameraList;

    public Vector3 Foward { get; private set; }
    public Vector3 PlaneFoward { get; private set; }
    public Vector3 Right { get; private set; }
    public Vector3 PlaneRight { get; private set; }

    public Transform User
    {
        get
        {
            return _user;
        }

        set
        {
            _user = value;
            _action.Invoke(_user);
        }
    }

    public void SetCameraDir(CameraController camera)
    {
        Foward = camera.transform.forward;
        Right = camera.transform.right;

        PlaneFoward = new Vector3(Foward.x, 0, Foward.z).normalized;
        PlaneRight = new Vector3(Right.x, 0, Right.z).normalized;
    }

    public void AddEventCamera(EventCamera eventCamera)
    {
        _eventCameraList.Add(eventCamera);
    }

    public EventCamera GetEventCamera(string path)
    {
        return _eventCameraList.First(e => e.Path == path);
    }
}
