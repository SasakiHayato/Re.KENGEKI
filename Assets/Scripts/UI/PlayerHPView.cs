using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHPView : MonoBehaviour
{
    Slider _slider;

    public void Setup(Slider slider)
    {
        _slider = slider;
    }

    public void CallBak(float value)
    {
        DOTween.To
            (
                () => _slider.value,
                (x) => _slider.value = x,
                value,
                0.2f
            );
    }
}
