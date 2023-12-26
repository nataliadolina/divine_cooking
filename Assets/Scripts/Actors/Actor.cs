using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    [SerializeField]
    private ActorType actorType;
    [SerializeField]
    private SpawnerType[] availableSpawnerTypes;

    public ActorType ActorType { get => actorType; }
    public SpawnerType[] AvailableSpawnerTypes { get => availableSpawnerTypes; }
}
