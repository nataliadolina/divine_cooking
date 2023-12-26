using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using System;

[Serializable]
public struct ActorWaitTime
{
    public Actor Actor;
    public float WaitTimeSeconds;

    public ActorWaitTime(Actor actor, float waitTimeSeconds)
    {
        Actor = actor;
        WaitTimeSeconds = waitTimeSeconds;
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float waitToStartTime;

    [SerializeField]
    private ActorWaitTime[] actorsWaitTimeMap;

    [SerializeField]
    private SpawnerTypesMap[] spawners;

    private CurrentFoodCookingProgressPool _foodProgress;

    private Queue<ActorWaitTime> _currentActorsPool = new Queue<ActorWaitTime>();
    private Queue<IFood> _currentFoodsPool = new Queue<IFood>();

    private void Start()
    {
        List<FoodType> _foodTypesInGame = new List<FoodType>();

        for (int i = 0; i < actorsWaitTimeMap.Length; i++)
        {
            ActorWaitTime actorWaitTime = actorsWaitTimeMap[i];
            Actor currentActor = actorWaitTime.Actor;
            SpawnerType st = currentActor.AvailableSpawnerTypes[UnityEngine.Random.Range(0, currentActor.AvailableSpawnerTypes.Length)];
            Transform spawner = spawners.Where(x => x.Key == st).FirstOrDefault().Value;
            Actor actor = Instantiate(currentActor, spawner.position, Quaternion.identity);
            actor.gameObject.SetActive(false);
            _currentActorsPool.Enqueue(new ActorWaitTime(actor, actorWaitTime.WaitTimeSeconds));

            if (actor.ActorType == ActorType.Food)
            {
                IFood food = actor.gameObject.GetComponent<IFood>();
                _currentFoodsPool.Enqueue(food);
                food.InitOnCreate(spawner);
                
                FoodType type = food.FoodType;
                if (!_foodTypesInGame.Contains(type))
                {
                    _foodTypesInGame.Add(type);
                }
            }
        }

        float maxScore = _currentFoodsPool.Select(x => x.MaxScore).Sum();
        FindObjectOfType<TotalScoreSlider>().SetMaxValue(maxScore);

        _foodProgress = FindObjectOfType<CurrentFoodCookingProgressPool>();
        _foodProgress.FillPool(_foodTypesInGame);

        StartCoroutine(WaitToSpawnNewPrefab());
    }

    private IEnumerator WaitToSpawnNewPrefab()
    {
        yield return new WaitForSeconds(waitToStartTime);

        while (_currentActorsPool.Count > 0)
        {
            ActorWaitTime actorWaitTime = _currentActorsPool.Dequeue();
            Actor currentActor = actorWaitTime.Actor;
            float waitTime = actorWaitTime.WaitTimeSeconds;
            currentActor.gameObject.SetActive(true);
            if (currentActor.ActorType == ActorType.Food)
            {
                IFood food = _currentFoodsPool.Dequeue();
                _foodProgress.ShowProgressSliderForFood(food);
            }

            yield return new WaitForSeconds(waitTime);
        }
        
        StopAllCoroutines();
    }
}
