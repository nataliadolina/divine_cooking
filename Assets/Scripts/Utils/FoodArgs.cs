using System.Collections.Generic;
using UnityEngine;

public struct FoodArgs
{
    public ActorType ActorType;
    public FoodCookingProgressSlider FoodCookingProgressSlider;
    public CookingAction[] CookingActions;
    public CookingAction CurrentCookingAction;
    public int CurrentCookingIndex;
    public float CurrentScore;
    public List<Object> Blades;
    public bool AdjustScale;
    public float ScorePerOneAction;
    public int RootInstanceId;
    public Color SplashColor;
    public SoundManager FruitSoundManager;
    public SplashParticles.Pool SplashParticlesPool;

    public FoodArgs(
        ActorType actorType,
        FoodCookingProgressSlider foodCookingProgressSlider,
        CookingAction[] cookingActions,
        CookingAction currentCookingAction,
        int currentCookingIndex,
        float currentScore,
        List<Object> blades,
        float scorePerOneAction,
        int rootInstanceId,
        Color splashColor,
        SoundManager fruitSoundManager,
        SplashParticles.Pool splashParticlesPool,
        bool adjustScale = false
        )
    {
        ActorType = actorType;
        FoodCookingProgressSlider = foodCookingProgressSlider;
        CookingActions = cookingActions;
        CurrentCookingAction = currentCookingAction;
        CurrentCookingIndex = currentCookingIndex;
        CurrentScore = currentScore;
        AdjustScale = adjustScale;
        ScorePerOneAction = scorePerOneAction;
        RootInstanceId = rootInstanceId;
        Blades = blades;
        SplashColor = splashColor;
        FruitSoundManager = fruitSoundManager;
        SplashParticlesPool = splashParticlesPool;
    }
}
