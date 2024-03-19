using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;
using System;

public class FoodInstaller : MonoInstaller
{
    [SerializeField]
    private ActorType actorType;

    [Inject]
    private GameInstaller.Settings _gameSettings;

    public override void InstallBindings()
    {
        InstallFoodSlicesPool();
        Container.Bind<SlicesSpawner>().AsSingle().NonLazy();
    }

    private void InstallFoodSlicesPool()
    {
        GameObject foodPrefab = _gameSettings.FoodActorTypeMap.Where(x => x.ActorType == actorType).FirstOrDefault().FoodPrefab;
        Dictionary<SlicePart, UnityEngine.Object> foodSlicesMap = new Dictionary<SlicePart, UnityEngine.Object>();

        foreach (var slicePart in (SlicePart[])Enum.GetValues(typeof(SlicePart)))
        {
            foodSlicesMap.Add(slicePart, foodPrefab);
        }

        Container.BindFactory<UnityEngine.Object, Food, Food.Factory>()
            .FromFactory<PrefabFactory<Food>>().NonLazy();

        Container.Bind<SlicesPool>()
            .AsCached()
            .WithArguments(GetComponent<SpriteRenderer>(), foodSlicesMap)
            .NonLazy();
    }
}