using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : IActorPoolSpawner
{
    public ActorGroupType ActorGroupType => ActorGroupType.Food;

    private DictionaryPool<ActorType, Food, Food.Factory> _foodPool;
    private CurrentFoodCookingProgressPool _foodProgress;

    public FoodSpawner(DictionaryPool<ActorType, Food, Food.Factory> foodPool, CurrentFoodCookingProgressPool foodProgress)
    {
        _foodPool = foodPool;
        _foodProgress = foodProgress;
    }

    public void FillPool(ActorType actorType, int num)
    {
        for (int i = 0; i < num; i++)
        {
            _foodPool.Create(actorType);
            _foodProgress.FillPool(actorType, 1);
        }
    }

    public GameObject Spawn(ActorType actorType, Vector3 position)
    {
        Food food = _foodPool.Spawn(actorType);
        food.transform.position = position;
        _foodProgress.ShowProgressSliderForFood(actorType, food);
        return food.gameObject;
    }
}
