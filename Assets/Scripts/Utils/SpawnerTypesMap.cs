using UnityEngine;
using System;

[Serializable]
public struct SpawnerTypesMap
{
    public SpawnerType Key;
    public Transform Value;

    public SpawnerTypesMap(SpawnerType spawnerType, Transform spawnerTransform)
    {
        Key = spawnerType;
        Value = spawnerTransform;
    }
}
