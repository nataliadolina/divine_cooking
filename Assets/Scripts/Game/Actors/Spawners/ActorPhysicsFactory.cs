using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsFactory
{
    private ActorSpringyPhysics.Factory _springyFactory;
    private ActorTransparentPhysics.Factory _transperantFactory;
    private ActorRealisticPhysics.Factory _realisticFactory;

    public ActorPhysicsFactory(
        ActorSpringyPhysics.Factory springyFactory,
        ActorTransparentPhysics.Factory transperantFactory,
        ActorRealisticPhysics.Factory realisticFactory
        )
    {
        _springyFactory = springyFactory;
        _transperantFactory = transperantFactory;
        _realisticFactory = realisticFactory;
    }

    public IActorPhysics CreatePhysics(PhysicsType type)
    {
        switch (type)
        {
            case PhysicsType.Springy:
                return _springyFactory.Create();

            case PhysicsType.Transperant:
                return _transperantFactory.Create();
            case PhysicsType.Realistic:
                return _realisticFactory.Create();
        }

        throw Assert.CreateException();
    }
}
