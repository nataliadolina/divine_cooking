using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IActor
{
    public ActorType ActorType { get; }
    public SpawnerType[] AvailableSpawnerTypes { get; }
}
