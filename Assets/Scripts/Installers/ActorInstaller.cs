using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public enum PhysicsType
{
    Standart,
    Springy
}

public class ActorInstaller : MonoInstaller
{
    [SerializeField]
    private Actor actor;

    public override void InstallBindings()
    {

    }

    [Serializable]
    public struct Settings
    {
        public PhysicsType PhysicsType;
    }
}
