using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class PlayerHPModel
{
    Slider _slider;

    public PlayerHPModel(Slider slider)
    {
        _slider = slider;
    }

    public void UpdateMaxValue(float value)
    {
        _slider.maxValue = value;
    }
}
