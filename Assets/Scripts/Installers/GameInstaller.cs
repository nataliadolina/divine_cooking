using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
public class GameInstaller : MonoInstaller
{
    [Inject]
    private Settings _settings;

    private Dictionary<ActorType, UnityEngine.Object> _foodTypesMap;

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
        _foodTypesMap = new Dictionary<ActorType, UnityEngine.Object>()
        {
            { ActorType.Food1, _settings.Food1 },
            { ActorType.Food2, _settings.Food2 },
            { ActorType.Food3, _settings.Food3 },
            { ActorType.Food4, _settings.Food4 },
            { ActorType.Food5, _settings.Food5 },
            { ActorType.Food6, _settings.Food6 },
            { ActorType.Food7, _settings.Food7 },
            { ActorType.Food8, _settings.Food8 },
        };

        Container.BindFactory<UnityEngine.Object, Food, Food.Factory>()
            .FromFactory<PrefabFactory<Food>>().NonLazy();

        Container.Bind<DictionaryPool<ActorType, Food, Food.Factory>>()
            .AsCached()
            .WithArguments(_foodTypesMap)
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
        public GameObject Food1;
        public GameObject Food2;
        public GameObject Food3;
        public GameObject Food4;
        public GameObject Food5;
        public GameObject Food6;
        public GameObject Food7;
        public GameObject Food8;
    }
}