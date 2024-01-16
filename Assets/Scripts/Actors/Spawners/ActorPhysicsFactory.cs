using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsFactory
{
    private ActorSpringyPhysics.Factory _springyFactory;
    private ActorTransparentPhysics.Factory _transperantFactory;

    public ActorPhysicsFactory(
        ActorSpringyPhysics.Factory springyFactory,
        ActorTransparentPhysics.Factory transperantFactory
        )
    {
        _springyFactory = springyFactory;
        _transperantFactory = transperantFactory;
    }

    public IActorPhysics CreatePhysics(PhysicsType type)
    {
        switch (type)
        {
            case PhysicsType.Springy:
                return _springyFactory.Create();

            case PhysicsType.Transperant:
                return _transperantFactory.Create();

        }

        throw Assert.CreateException();
    }
}
