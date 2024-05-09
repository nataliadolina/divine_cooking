using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct DoubleSliderType
{
    public ActorType ActorType;
    public FoodCookingProgressSlider ProgressBar;
}

public class FoodCookingProgressSlider : MonoBehaviour
{
    [SerializeField]
    private ActorType foodType;

    [SerializeField]
    private Slider successSlider;

    [SerializeField]
    private Slider failureSlider;

    public void Init(float maxScore)
    {
        successSlider.maxValue = maxScore;
        failureSlider.maxValue = maxScore;
        gameObject.SetActive(true);
    }

    public void AddScore(float value = 1)
    {
        successSlider.value+=value;
        failureSlider.value+=value;
    }

    public void SubScore(float value = 1)
    {
        successSlider.value -= value;
    }

    public void Release()
    {
        gameObject.SetActive(false);
        successSlider.maxValue = 0;
        failureSlider.maxValue = 0;
    }
}
