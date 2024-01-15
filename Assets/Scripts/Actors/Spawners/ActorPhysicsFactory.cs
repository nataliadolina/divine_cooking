using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsFactory
{
    private ActorSpringyPhysics.Factory _springyFactory;

    public ActorPhysicsFactory(ActorSpringyPhysics.Factory springyFactory)
    {
        _springyFactory = springyFactory;
    }

    public IActorPhysics CreatePhysics(PhysicsType type)
    {
        switch (type)
        {
            case PhysicsType.Springy:
                return _springyFactory.Create();
        }

        throw Assert.CreateException();
    }
}
