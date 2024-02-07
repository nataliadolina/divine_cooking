using System.Collections.Generic;
using UnityEngine;

public struct FoodArgs
{
    public FoodCookingProgressSlider FoodCookingProgressSlider;
    public CookingAction CurrentCookingAction;
    public int CurrentCookingIndex;
    public float CurrentScore;
    public List<Object> Blades;
    public float ScorePerOneAction;
    public int RootInstanceId;
    public Vector2 Velocity;
    public Vector3 Direction;
    public float Speed;

    public FoodArgs(
        FoodCookingProgressSlider foodCookingProgressSlider,
        CookingAction currentCookingAction,
        int currentCookingIndex,
        float currentScore,
        List<Object> blades,
        float scorePerOneAction,
        Vector2 velocity,
        int rootInstanceId,
        Vector3 direction,
        float speed
        )
    {
        FoodCookingProgressSlider = foodCookingProgressSlider;
        CurrentCookingAction = currentCookingAction;
        CurrentCookingIndex = currentCookingIndex;
        CurrentScore = currentScore;
        ScorePerOneAction = scorePerOneAction;
        RootInstanceId = rootInstanceId;
        Blades = blades;
        Velocity = velocity;
        Direction = direction;
        Speed = speed;
    }
}
