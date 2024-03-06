using System.Collections.Generic;
using UnityEngine;

public struct FoodArgs
{
    public FoodCookingProgressSlider FoodCookingProgressSlider;
    public CookingAction CurrentCookingAction;
    public int CurrentCookingIndex;
    public float CurrentScore;
    public List<Object> Blades;
    public float PartOfOne;
    public int RootInstanceId;
    public Vector2 Velocity;
    public Vector3 Direction;
    public float Speed;
    public float CurrentTotalScore;
    public List<UnityEngine.Object> Ricochets;

    public FoodArgs(
        FoodCookingProgressSlider foodCookingProgressSlider,
        CookingAction currentCookingAction,
        int currentCookingIndex,
        float currentScore,
        List<Object> blades,
        float partOfOne,
        Vector2 velocity,
        int rootInstanceId,
        Vector3 direction,
        float speed,
        float currentTotalScore,
        List<UnityEngine.Object> ricochets
        )
    {
        FoodCookingProgressSlider = foodCookingProgressSlider;
        CurrentCookingAction = currentCookingAction;
        CurrentCookingIndex = currentCookingIndex;
        CurrentScore = currentScore;
        PartOfOne = partOfOne;
        RootInstanceId = rootInstanceId;
        Blades = blades;
        Velocity = velocity;
        Direction = direction;
        Speed = speed;
        CurrentTotalScore = currentTotalScore;
        Ricochets = ricochets;
    }
}
