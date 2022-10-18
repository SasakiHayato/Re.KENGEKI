using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public enum CmInputType
{
    Standard,
    Reversal,
}

/// <summary>
/// カメラのデータクラス
/// </summary>

public class CameraInfomation
{
    public CameraInfomation(Action<Transform> action)
    {
        _action = action;
        _eventCameraList = new List<EventCamera>();
        CmTargetList = new List<ICmTarget>();
    }

    Transform _user;
    Action<Transform> _action;
    List<EventCamera> _eventCameraList;
    
    public List<ICmTarget> CmTargetList { get; private set; }
    public Vector3 PlaneFoward { get; private set; }
    public Vector3 PlaneRight { get; private set; }
    public CmInputType AxisHrosontal { get; set; }
    public CmInputType AxisVerticle { get; set; }

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
        Vector3 foward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        PlaneFoward = new Vector3(foward.x, 0, foward.z).normalized;
        PlaneRight = new Vector3(right.x, 0, right.z).normalized;
    }

    public void AddEventCamera(EventCamera eventCamera)
    {
        _eventCameraList.Add(eventCamera);
    }

    public EventCamera GetEventCamera(string path)
    {
        return _eventCameraList.First(e => e.Path == path);
    }

    public void AddITarget(ICmTarget target)
    {
        CmTargetList.Add(target);
    }
}
