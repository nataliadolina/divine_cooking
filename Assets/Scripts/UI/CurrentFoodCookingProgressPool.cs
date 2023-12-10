using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CurrentFoodCookingProgressPool : MonoBehaviour
{
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private DoubleSliderType[] _doubleSliderTypesMap;

    private Dictionary<FoodType, FoodCookingProgressSlider> _progressTypesHashMap = new Dictionary<FoodType, FoodCookingProgressSlider>();
    private Dictionary<int, FoodCookingProgressSlider> _foodProgressMap = new Dictionary<int, FoodCookingProgressSlider>();
    private Dictionary<FoodType, List<FoodCookingProgressSlider>> _freeProgress = new Dictionary<FoodType, List<FoodCookingProgressSlider>>();

    public void FillPool(List<FoodType> food)
    {
        foreach (FoodType foodType in food)
        {
            _freeProgress.Add(foodType, new List<FoodCookingProgressSlider>());
            for (int i = 0; i < poolSize; i++)
            {
                FoodCookingProgressSlider prefab = GetSliderPrefabByType(foodType);
                FoodCookingProgressSlider progress = Instantiate(prefab, transform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(progress.GetComponent<RectTransform>());

                progress.gameObject.SetActive(false);
                _freeProgress[foodType].Add(progress);
            }
        }
    }

    public void ShowProgressSliderForFood(Food food)
    {
        FoodCookingProgressSlider progress;
        List<FoodCookingProgressSlider> value;
        if (!(_freeProgress.TryGetValue(food.FoodType, out value) && value.Count > 0))
        {
            FoodCookingProgressSlider prefab = GetSliderPrefabByType(food.FoodType);
            progress = Instantiate(prefab, transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(progress.GetComponent<RectTransform>());
        }
        else
        {
            progress = value[0];
            _freeProgress[food.FoodType].Remove(progress);
        }

        food.ProgressSlider = progress;
        progress.transform.SetAsFirstSibling();
        progress.Init(food.MaxScore);
        _foodProgressMap.Add(food.RootInstanceId, progress);
    }

    private FoodCookingProgressSlider GetSliderPrefabByType(FoodType foodType)
    {
        if (_progressTypesHashMap.ContainsKey(foodType))
        {
            return _progressTypesHashMap.GetValueOrDefault(foodType);
        }

        var fcps = _doubleSliderTypesMap.Where(x => x.FoodType == foodType).FirstOrDefault().ProgressBar;
        _progressTypesHashMap.Add(foodType, fcps);
        return fcps;
    }

    public void ReleaseDoubleSlider(Food food)
    {
        int id = food.RootInstanceId;
        if (!_foodProgressMap.ContainsKey(id))
        {
            return;
        }

        var progress = _foodProgressMap[id];
        progress.Release();
        _foodProgressMap.Remove(id);
        FoodType key = food.FoodType;

        if (!_freeProgress.ContainsKey(key))
        {
            _freeProgress.Add(key, new List<FoodCookingProgressSlider>());
        }

        _freeProgress[key].Add(progress);
    }
}
