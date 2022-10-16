using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Image _filter;

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
        CreateFilter();
    }

    void CreateFilter()
    {
        GameObject obj = new GameObject("Filter");
        obj.transform.SetParent(transform);

        _filter = obj.AddComponent<Image>();
        _filter.rectTransform.anchorMax = Vector2.one;
        _filter.rectTransform.anchorMin = Vector2.zero;
        _filter.rectTransform.offsetMax = Vector2.zero;
        _filter.rectTransform.offsetMin = Vector2.zero;

        Color color = new Color(0, 0, 0, 0);
        _filter.color = color;
    }

    public void Filter(bool isFilter)
    {
        _filter.raycastTarget = isFilter;
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
