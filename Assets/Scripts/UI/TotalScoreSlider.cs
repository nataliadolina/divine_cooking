using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TotalScoreSlider : MonoBehaviour
{
    private Slider _slider;

    [Inject]
    private void Construct()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
    }

    public void SetMaxScore(float score)
    {
        _slider.maxValue = score;
    }

    public void ChangeScore(float value)
    {
        _slider.value += value;
    }
}
