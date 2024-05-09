using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class ActorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindFactory<ActorSpringyPhysics, ActorSpringyPhysics.Factory>()
        .AsCached()
        .NonLazy();

        Container.BindFactory<ActorTransparentPhysics, ActorTransparentPhysics.Factory>()
            .AsCached()
            .NonLazy();

        Container.BindFactory<ActorRealisticPhysics, ActorRealisticPhysics.Factory>()
            .AsCached()
            .NonLazy();

        Container.BindFactory<ActorFreezeRotationPhysics, ActorFreezeRotationPhysics.Factory>()
            .AsCached()
            .NonLazy();
        Container.BindFactory<ActorEmptyPhysics, ActorEmptyPhysics.Factory>()
            .AsCached()
            .NonLazy();

        Container.Bind<ActorPhysicsFactory>().AsSingle().NonLazy();
    }
}
