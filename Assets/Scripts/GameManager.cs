using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{ 
    [SerializeField]
    private Food[] foods;
    [SerializeField]
    private SpawnerTypesMap[] spawners;

    private CurrentFoodCookingProgressPool _foodProgress;

    private Queue<Food> _currentFoodsPool = new Queue<Food>();

    private void Start()
    {
        List<FoodType> _foodTypesInGame = new List<FoodType>();

        for (int i = 0; i < foods.Length; i++)
        {
            Food currentFood = foods[i];
            SpawnerType st = currentFood.AvailableSpawnerTypes[Random.Range(0, currentFood.AvailableSpawnerTypes.Length)];
            Transform spawner = spawners.Where(x => x.Key == st).FirstOrDefault().Value;
            Food food = Instantiate(currentFood, spawner.position, Quaternion.identity);
            food.InitOnCreate(spawner);
            food.gameObject.SetActive(false);

            _currentFoodsPool.Enqueue(food);

            FoodType type = currentFood.FoodType;
            if (!_foodTypesInGame.Contains(type))
            {
                _foodTypesInGame.Add(type);
            }
        }

        _foodProgress = FindObjectOfType<CurrentFoodCookingProgressPool>();
        _foodProgress.FillPool(_foodTypesInGame);

        StartCoroutine(WaitToSpawnNewPrefab());
    }

    private IEnumerator WaitToSpawnNewPrefab()
    {
        while (_currentFoodsPool.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            Food currentFood = _currentFoodsPool.Dequeue();
            currentFood.gameObject.SetActive(true);
            _foodProgress.ShowProgressSliderForFood(currentFood);

        }
        
        StopAllCoroutines();
    }
}
