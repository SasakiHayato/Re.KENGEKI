using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class UIPresenter : MonoBehaviour, IManager
{
    List<WindowBase> _windowList;

    ReactiveProperty<bool> _reactiveBool = new ReactiveProperty<bool>();

    void Awake()
    {
        _windowList = new List<WindowBase>();

        for (int index = 0; index < transform.childCount; index++)
        {
            WindowBase window = transform.GetChild(index).GetComponent<WindowBase>();

            if (window != null)
            {
                _windowList.Add(window);
            }
        }

        GameManager.Instance.AddManager(this);
    }

    void Start()
    {
        _reactiveBool
            .ObserveEveryValueChanged(_ => Gamepad.current != null)
            .Subscribe(isConnect => SetFilter(isConnect))
            .AddTo(this);
    }

    void SetFilter(bool isFilter)
    {
        _windowList.ForEach(w => w.Filter(isFilter));
    }

    public void ViewUpdate(WindowType type, string path, object[] values = null)
    {
        PresenterBase presenter = _windowList
            .First(w => w.WindowType == type)
            .PresenterList.First(p => p.Path == path);
        
        presenter.CallBackView(values);
    }

    public void ModelUpdate(WindowType type, string path, object[] values = null)
    {
        PresenterBase presenter = _windowList
            .First(w => w.WindowType == type)
            .PresenterList.First(p => p.Path == path);

        presenter.CallBackModel(values);
    }

    public void SetActive(WindowType type, bool isActive)
    {
        _windowList.First(w => w.WindowType == type).Active(isActive);
    }

    // ‰º‹L, IManager
    public string Key => nameof(UIPresenter);
    public Object Type()
    {
        return this;
    }
}
