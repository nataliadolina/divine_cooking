using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class ActorInstaller : MonoInstaller
{
    [Inject]
    private Actor.Settings _actorSettings;
    public override void InstallBindings()
    {
        if (_actorSettings.PhysicsType.HasFlag(PhysicsType.Springy))
        {
            Container.BindFactory<ActorSpringyPhysics, ActorSpringyPhysics.Factory>()
            .AsCached()
            .NonLazy();
        }

        Container.Bind<ActorPhysicsFactory>().AsSingle().NonLazy();
    }
}
