using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventCmRequest : MonoBehaviour
{
    [SerializeField] string _requestPath;
    [SerializeField] float _durationTime = 0.2f;

    CameraController _cameraController;

    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    public void Request()
    {
        _cameraController.TransitionEventCm(_requestPath, _durationTime);
    }

    public void CallBack()
    {
        _cameraController.CallBackTransition(_durationTime);
    }
}
