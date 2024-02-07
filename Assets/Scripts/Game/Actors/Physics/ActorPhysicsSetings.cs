using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActorPhysicsSettings
{
    private static Dictionary<int, ActorSpringyPhysics.Settings> _actorSpringySettingsMap = new Dictionary<int, ActorSpringyPhysics.Settings>();
    public static void SetSpringyPhysicsSettings(int id, ActorSpringyPhysics.Settings settings)
    {
        if (!_actorSpringySettingsMap.TryAdd(id, settings))
        {
            UpdateSpringyPhysicsSettings(id, settings);
        }
    }

    public static ActorSpringyPhysics.Settings GetSpringyPhysicsSettingsById(int id)
    {
        if (_actorSpringySettingsMap.ContainsKey(id)){
            return _actorSpringySettingsMap[id];
        }
        return new ActorSpringyPhysics.Settings(Vector2.zero, 0);
    }

    public static void UpdateSpringyPhysicsSettings(int id, ActorSpringyPhysics.Settings settings)
    {
        _actorSpringySettingsMap[id] = settings;
    }

    public static void DeleteSpringyPhysicsSettings(int id)
    {
        if (_actorSpringySettingsMap.ContainsKey(id))
        {
            _actorSpringySettingsMap.Remove(id); 
        }
    }
}
