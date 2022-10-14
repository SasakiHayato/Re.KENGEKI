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

    List<ViewBase> _viewList;
    CanvasGroup _canvasGroup;

    public WindowType WindowType => _windowType;

    public List<ViewBase> ViewList => _viewList;
 
    void Awake()
    {
        _viewList = new List<ViewBase>();

        for (int index = 0; index < transform.childCount; index++)
        {
            ViewBase view = transform.GetChild(index).GetComponent<ViewBase>();
            _viewList.Add(view);
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
