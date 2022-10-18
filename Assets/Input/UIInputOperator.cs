using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public interface IUIInputEvent
{
    GameInputType GameInputType { get; }
    void OnEnable();
    void Submit();
    void Select(int inputX, int inputY);
    bool Cancel();
    void Initalize();
}

public class UIInputOperator : MonoBehaviour, IManager
{
    [SerializeField] float _sencivity;

    InputOperator _inputOperator;
    
    IUIInputEvent _operator;
    Func<bool> _isExecution;

    void Awake()
    {
        _inputOperator = new InputOperator();
        _inputOperator.Enable();

        GameManager.Instance.AddManager(this);
    }

    void Start()
    {
        _inputOperator.UI.Submit.performed += contextMenu => Submit();
        _inputOperator.UI.Cancel.performed += contextMenu => Cancel();

        _inputOperator.UI.Select
            .ObserveEveryValueChanged(_ => _.ReadValue<Vector2>())
            .Subscribe(_ => Select(_))
            .AddTo(this);
    }

    void Update()
    {
        if (_operator == null || _isExecution == null)
        {
            return;
        }

        if (!_isExecution.Invoke())
        {
            _operator.Initalize();
            _operator = null;
        }
    }

    private void OnDestroy()
    {
        _inputOperator.Disable();
    }

    void Select(Vector2 dir)
    {
        if (_operator == null)
        {
            return;
        }

        if (Mathf.Abs(dir.x) < _sencivity)
        {
            dir.x = 0;
        }
        else
        {
            dir.x = 1 * Mathf.Sign(dir.x);
        }
        
        if (Mathf.Abs(dir.y) < _sencivity)
        {
            dir.y = 0;
        }
        else
        {
            dir.y = 1 * Mathf.Sign(dir.y);
        }

        _operator.Select((int)dir.x, (int)dir.y);
    }

    void Submit()
    {
        if (_operator != null)
        {
            _operator.Submit();
            _operator.Initalize();
            _operator = null;
        }
    }

    void Cancel()
    {
        if (_operator != null)
        {
            if (_operator.Cancel())
            {
                _operator.Initalize();
                _operator = null;
            }
        }
    }

    public void RequestOperation(IUIInputEvent uiEvent, Func<bool> action = null)
    {
        GameManager.Instance.InputType = uiEvent.GameInputType;
        _isExecution = action;

        _operator = uiEvent;
        uiEvent.OnEnable();
    }

    // ‰º‹L, IManager
    public string Key => nameof(UIInputOperator);
    public UnityEngine.Object Type()
    {
        return this;
    }
}
