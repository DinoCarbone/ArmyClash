using System;
using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public interface IDestructionService
    {
        void Destruct(GameObject coreObject);
    }
}