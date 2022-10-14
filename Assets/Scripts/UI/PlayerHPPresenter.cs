using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPPresenter : PresenterBase
{
    [SerializeField] PlayerHPView _playerHPView;

    PlayerHPModel _model;

    protected override void Setup()
    {
        Slider slider = GetComponent<Slider>();
        _playerHPView.Setup(slider);
        _model = new PlayerHPModel(slider);
    }

    public override void CallBackModel(object[] values)
    {
        _model.UpdateMaxValue((int)values[0]);
    }

    public override void CallBackView(object[] values)
    {
        _playerHPView.CallBak((int)values[0]);
    }
}
