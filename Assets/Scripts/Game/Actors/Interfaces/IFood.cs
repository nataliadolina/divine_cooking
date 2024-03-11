using UnityEngine;

public interface IFood : IActor
{
    public FoodCookingProgressSlider ProgressSlider { set; }
    public int MaxScore { get; }
    public void Despawn();
}
