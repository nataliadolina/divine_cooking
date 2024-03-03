using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CurrentFoodCookingProgressPool : MonoBehaviour
{
    private struct FoodSliderPoolObject
    {
        public FoodCookingProgressSlider Slider;
        public RectTransform RectTransform;
    }

    [SerializeField]
    private int poolSize;
    [SerializeField]
    private DoubleSliderType[] _doubleSliderTypesMap;

    private Dictionary<ActorType, FoodCookingProgressSlider> _progressTypesHashMap = new Dictionary<ActorType, FoodCookingProgressSlider>();
    private Dictionary<int, FoodSliderPoolObject> _foodProgressMap = new Dictionary<int, FoodSliderPoolObject>();
    private Dictionary<ActorType, List<FoodSliderPoolObject>> _freeProgress = new Dictionary<ActorType, List<FoodSliderPoolObject>>();

    public void FillPool(ActorType foodType, int num)
    {
        if (!_freeProgress.ContainsKey(foodType))
        {
            _freeProgress.Add(foodType, new List<FoodSliderPoolObject>());
        }

        for (int i = 0; i < num; i++)
        {
            FoodCookingProgressSlider prefab = GetSliderPrefabByType(foodType);
            FoodCookingProgressSlider progress = Instantiate(prefab, transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(progress.GetComponent<RectTransform>());

            progress.gameObject.SetActive(false);
            _freeProgress[foodType].Add(new FoodSliderPoolObject { Slider = progress, RectTransform = progress.GetComponent<RectTransform>() });
        }
    }

    public FoodCookingProgressSlider ShowProgressSliderForFood(ActorType actorType, IFood food)
    {
        FoodCookingProgressSlider progress;
        FoodSliderPoolObject poolObject;
        List<FoodSliderPoolObject> value;
        if (!(_freeProgress.TryGetValue(actorType, out value) && value.Count > 0))
        {
            FoodCookingProgressSlider prefab = GetSliderPrefabByType(actorType);
            progress = Instantiate(prefab, transform);
            poolObject = new FoodSliderPoolObject { Slider = progress, RectTransform = progress.GetComponent<RectTransform>() };
        }
        else
        {
            poolObject = value[0];
            progress = poolObject.Slider;
            _freeProgress[actorType].Remove(poolObject);
        }

        food.ProgressSlider = progress;
        LayoutRebuilder.ForceRebuildLayoutImmediate(poolObject.RectTransform);
        progress.transform.SetAsFirstSibling();
        progress.Init(food.MaxScore);
        _foodProgressMap.Add(food.RootInstanceId, poolObject);
        LayoutRebuilder.ForceRebuildLayoutImmediate(poolObject.RectTransform);
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

        FoodSliderPoolObject poolObject = _foodProgressMap[id];
        var progress = poolObject.Slider;
        progress.Release();
        _foodProgressMap.Remove(id);
        ActorType key = food.ActorType;

        if (!_freeProgress.ContainsKey(key))
        {
            _freeProgress.Add(key, new List<FoodSliderPoolObject>());
        }

        _freeProgress[key].Add(poolObject);
    }
}
