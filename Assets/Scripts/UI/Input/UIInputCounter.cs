using UnityEngine;
using System;

public class UIInputCounter : IUIInputEvent
{
    Player _player;
    UIPresenter _ui;
    Action _action;

    int _inputValue;

    public GameInputType GameInputType => GameInputType.UI;

    public UIInputCounter(Action action = null)
    {
        _action = action;
    }

    public void OnEnable()
    {
        _ui = GameManager.Instance.GetManager<UIPresenter>(nameof(UIPresenter));
        _ui.ViewUpdate(WindowType.Game, "CounterText", new object[] { true, 0 });

        _player = GameManager.Instance.GameUser.GetComponent<Player>();

        Time.timeScale = 0.1f;
    }

    public void Select(int inputX, int inputY)
    {
        if (inputY == 0)
        {
            return;
        }

        _inputValue += inputY;

        if (_inputValue < 0)
        {
            _inputValue = 0;
        }

        if (_inputValue > 1)
        {
            _inputValue = 1;
        }

        _ui.ViewUpdate(WindowType.Game, "CounterText", new object[] { true, _inputValue });
    }

    public void Submit()
    {
        _player.OnCounter(_inputValue);
    }

    public bool Cancel()
    {
        return false;
    }

    public void Initalize()
    {
        _ui.ViewUpdate(WindowType.Game, "CounterText", new object[] { false });

        GameManager.Instance.GetManager<CameraController>(nameof(CameraController)).CallBackTransition();
        GameManager.Instance.InputType = GameInputType.Player;

        _action?.Invoke();

        Time.timeScale = 1;
    }
}
