using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreSlider : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public void ChangeScore(float value)
    {
        _slider.value += value;
    }
}
