using UnityEngine;

public class CounterTextPresenter : PresenterBase
{
    [SerializeField] CounterTextView _counterTextView;

    protected override void Setup()
    {
        _counterTextView.Setup();
        _counterTextView.gameObject.SetActive(false);
    }

    public override void CallBackModel(object[] values)
    {
        
    }

    public override void CallBackView(object[] values)
    {
        if ((bool)values[0])
        {
            _counterTextView.gameObject.SetActive(true);
            _counterTextView.ViewUpdate((int)values[1]);
        }
        else
        {
            _counterTextView.gameObject.SetActive(false);
        }
    }
}
