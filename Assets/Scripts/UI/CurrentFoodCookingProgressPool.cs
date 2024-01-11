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

    private List<ActorType> _foodTypesInGame = new List<ActorType>();
    private Dictionary<ActorType, FoodCookingProgressSlider> _progressTypesHashMap = new Dictionary<ActorType, FoodCookingProgressSlider>();
    private Dictionary<int, FoodCookingProgressSlider> _foodProgressMap = new Dictionary<int, FoodCookingProgressSlider>();
    private Dictionary<ActorType, List<FoodCookingProgressSlider>> _freeProgress = new Dictionary<ActorType, List<FoodCookingProgressSlider>>();

    public void FillPool(ActorType foodType, int num)
    {
        if (!_freeProgress.ContainsKey(foodType))
        {
            _freeProgress.Add(foodType, new List<FoodCookingProgressSlider>());
        }

        for (int i = 0; i < num; i++)
        {
            FoodCookingProgressSlider prefab = GetSliderPrefabByType(foodType);
            FoodCookingProgressSlider progress = Instantiate(prefab, transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(progress.GetComponent<RectTransform>());

            progress.gameObject.SetActive(false);
            _freeProgress[foodType].Add(progress);
        }
    }

    public FoodCookingProgressSlider ShowProgressSliderForFood(ActorType actorType, IFood food)
    {
        FoodCookingProgressSlider progress;
        List<FoodCookingProgressSlider> value;
        if (!(_freeProgress.TryGetValue(actorType, out value) && value.Count > 0))
        {
            FoodCookingProgressSlider prefab = GetSliderPrefabByType(actorType);
            progress = Instantiate(prefab, transform);
        }
        else
        {
            progress = value[0];
            _freeProgress[actorType].Remove(progress);
        }

        food.ProgressSlider = progress;
        progress.transform.SetAsFirstSibling();
        progress.Init(food.MaxScore);
        _foodProgressMap.Add(food.RootInstanceId, progress);
        LayoutRebuilder.ForceRebuildLayoutImmediate(progress.GetComponent<RectTransform>());
        return progress;
    }

    private FoodCookingProgressSlider GetSliderPrefabByType(ActorType actorType)
    {
        if (_progressTypesHashMap.ContainsKey(actorType))
        {
            return _progressTypesHashMap.GetValueOrDefault(actorType);
        }

        var fcps = _doubleSliderTypesMap.Where(x => x.ActorType == actorType).FirstOrDefault().ProgressBar;
        _progressTypesHashMap.Add(actorType, fcps);
        return fcps;
    }

    public void ReleaseDoubleSlider(IFood food)
    {
        int id = food.RootInstanceId;
        if (!_foodProgressMap.ContainsKey(id))
        {
            return;
        }

        var progress = _foodProgressMap[id];
        progress.Release();
        _foodProgressMap.Remove(id);
        ActorType key = food.ActorType;

        if (!_freeProgress.ContainsKey(key))
        {
            _freeProgress.Add(key, new List<FoodCookingProgressSlider>());
        }

        _freeProgress[key].Add(progress);
    }
}
