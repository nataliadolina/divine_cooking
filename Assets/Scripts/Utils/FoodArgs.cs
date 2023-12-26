using System.Collections.Generic;
using UnityEngine;

public struct FoodArgs
{
    public FoodType FoodType;
    public FoodCookingProgressSlider FoodCookingProgressSlider;
    public CookingAction[] CookingActions;
    public CookingAction CurrentCookingAction;
    public int CurrentCookingIndex;
    public float CurrentScore;
    public List<Object> Blades;
    public bool AdjustScale;
    public float ScorePerOneAction;
    public int RootInstanceId;
    public Vector2 Velocity;
    public Color SplashColor;

    public FoodArgs(
        FoodType foodType,
        FoodCookingProgressSlider foodCookingProgressSlider,
        CookingAction[] cookingActions,
        CookingAction currentCookingAction,
        int currentCookingIndex,
        float currentScore,
        List<Object> blades,
        float scorePerOneAction,
        int rootInstanceId,
        Vector2 velocity,
        Color splashColor,
        bool adjustScale = false
        )
    {
        FoodType = foodType;
        FoodCookingProgressSlider = foodCookingProgressSlider;
        CookingActions = cookingActions;
        CurrentCookingAction = currentCookingAction;
        CurrentCookingIndex = currentCookingIndex;
        CurrentScore = currentScore;
        AdjustScale = adjustScale;
        ScorePerOneAction = scorePerOneAction;
        RootInstanceId = rootInstanceId;
        Velocity = velocity;
        Blades = blades;
        SplashColor = splashColor;
    }
}
