using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPresenter : MonoBehaviour
{
    List<WindowBase> _windowList;

    void Awake()
    {
        _windowList = new List<WindowBase>();

        for (int index = 0; index < transform.childCount; index++)
        {
            WindowBase window = transform.GetChild(index).GetComponent<WindowBase>();
            _windowList.Add(window);
        }
    }

    public void UIUpdate(WindowType type, string path, object[] values = null)
    {
        ViewBase view = _windowList
            .First(w => w.WindowType == type)
            .ViewList.First(v => v.Path == path);

        view.CallBack(values);
    }

    public void SetActive(WindowType type, bool isActive)
    {
        _windowList.First(w => w.WindowType == type).Active(isActive);
    }
}
