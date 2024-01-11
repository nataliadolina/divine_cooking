using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum ActorGroupType
{
    Bomb,
    Food
}

public interface IActorPoolSpawner
{
    public ActorGroupType ActorGroupType { get; }
    public void FillPool(ActorType actor, int num);
    public GameObject Spawn(ActorType actorType, Vector3 position);
}
