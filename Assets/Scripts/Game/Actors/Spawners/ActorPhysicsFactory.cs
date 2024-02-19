using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsFactory
{
    private ActorSpringyPhysics.Factory _springyFactory;
    private ActorTransparentPhysics.Factory _transperantFactory;
    private ActorRealisticPhysics.Factory _realisticFactory;
    private ActorFreezeRotationPhysics.Factory _freezeRotationFactory;

    public ActorPhysicsFactory(
        ActorSpringyPhysics.Factory springyFactory,
        ActorTransparentPhysics.Factory transperantFactory,
        ActorRealisticPhysics.Factory realisticFactory,
        ActorFreezeRotationPhysics.Factory freezeRotationFactory
        )
    {
        _springyFactory = springyFactory;
        _transperantFactory = transperantFactory;
        _realisticFactory = realisticFactory;
        _freezeRotationFactory = freezeRotationFactory;
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
            case PhysicsType.FreezeRotation:
                return _freezeRotationFactory.Create();
        }

        throw Assert.CreateException();
    }
}
