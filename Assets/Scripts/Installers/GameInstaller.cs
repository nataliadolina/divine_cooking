using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

[Serializable]
public struct FoodActorType
{
    public ActorType ActorType;
    public GameObject FoodPrefab;
}

public class GameInstaller : MonoInstaller
{
    [Inject]
    private Settings _settings;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<SplashParticles, SplashParticles.Pool>()
            .FromComponentInNewPrefab(_settings.SplashParticles)
            .UnderTransformGroup("Splashes")
            .AsCached()
            .NonLazy();

        Container.BindMemoryPool<ExplosionParticles, ExplosionParticles.Pool>()
            .FromComponentInNewPrefab(_settings.ExplosionParticles)
            .UnderTransformGroup("Explosions")
            .AsCached()
            .NonLazy();

        Container.BindMemoryPool<Bomb, Bomb.Pool>()
            .FromComponentInNewPrefab(_settings.Bomb)
            .UnderTransformGroup("Bombs")
            .AsCached()
            .NonLazy();

        InstallFoodPool();

        InstallSpawners();
    }

    private void InstallFoodPool()
    {
        Dictionary<ActorType, UnityEngine.Object> foodTypesMap = new Dictionary<ActorType, UnityEngine.Object>();
        
        foreach (var foodActorType in _settings.FoodActorTypeMap)
        {
            foodTypesMap.Add(foodActorType.ActorType, foodActorType.FoodPrefab);
        }

        Container.BindFactory<UnityEngine.Object, Food, Food.Factory>()
            .FromFactory<PrefabFactory<Food>>().NonLazy();

        Container.Bind<DictionaryPool<ActorType, Food, Food.Factory>>()
            .AsCached()
            .WithArguments(foodTypesMap)
            .NonLazy();
    }

    private void InstallSpawners()
    {
        Container.BindInterfacesAndSelfTo<BombSpawner>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<FoodSpawner>().AsCached().NonLazy();
        Container.Bind<ActorPoolFactory>().AsSingle().NonLazy();
    }

    [Serializable]
    public struct Settings
    {
        public GameObject SplashParticles;
        public GameObject ExplosionParticles;
        public GameObject Bomb;
        public FoodActorType[] FoodActorTypeMap;
    }
}