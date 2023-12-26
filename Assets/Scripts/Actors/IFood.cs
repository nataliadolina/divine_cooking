using UnityEngine;

public interface IFood
{
    public FoodType FoodType { get; }
    public void InitOnCreate(Transform parentTransform);
    public FoodCookingProgressSlider ProgressSlider { set; }
    public int MaxScore { get; }
    public int RootInstanceId { get; }
}
