using System;
using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public class MockDeathService : IDeathService
    {
        /// <summary>Тестовая реализация сервиса смерти — логирует регистрацию смертей.</summary>
        public void RegisterDeath(IKillableData killable)
        {
            Debug.Log("RegisterDeath by type: " + killable.GetType().Name);
        }
    }
    public class DeathService : IDeathService
    {
        public void RegisterDeath(IKillableData killable)
        {
            throw new NotImplementedException();
        }
    }
}