using UnityEngine;

public class TestUIRequest : MonoBehaviour
{
    [SerializeField] WindowType _windowType;
    [SerializeField] string _path;
    [SerializeField] int _intValue;

    void Start()
    {
        
    }

    public void RequestView()
    {
        UIPresenter presenter = GameManager.Instance.GetManager<UIPresenter>(nameof(UIPresenter));
        presenter.ViewUpdate(_windowType, _path, new object[] { _intValue });
    }

    public void RequestModel()
    {
        UIPresenter presenter = GameManager.Instance.GetManager<UIPresenter>(nameof(UIPresenter));
        presenter.ModelUpdate(_windowType, _path, new object[] { _intValue });
    }
}
