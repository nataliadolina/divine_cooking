using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class ActorPoolFactory
{
    private readonly DiContainer _container;

    public ActorPoolFactory(DiContainer container) =>
      _container = container;

    public IActorPoolSpawner CreateSpawner(ActorGroupType actorType)
    {
        return _container.ResolveAll<IActorPoolSpawner>().Where(x => x.ActorGroupType == actorType).FirstOrDefault();
    }
      
}
