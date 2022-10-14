using System.Collections.Generic;
using UnityEngine;

public enum WindowType
{
    Game,
    Option,
}

public abstract class WindowBase : MonoBehaviour
{
    [SerializeField] WindowType _windowType;

    List<PresenterBase> _presenterList;
    CanvasGroup _canvasGroup;

    public WindowType WindowType => _windowType;

    public List<PresenterBase> PresenterList => _presenterList;
 
    void Awake()
    {
        _presenterList = new List<PresenterBase>();

        for (int index = 0; index < transform.childCount; index++)
        {
            PresenterBase view = transform.GetChild(index).GetComponent<PresenterBase>();
            _presenterList.Add(view);
        }

        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void Active(bool isActive)
    {
        if (isActive)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = 0;
        }
    }
}
