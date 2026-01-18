using System;

namespace Core.Behaviors.Health
{
    public interface IHealthProvider
    {
        int MaxHealth { get; }
        int Health { get; }

        event Action<int> OnChangeHealth;
        event Action<int> OnChangeMaxHealth;
    }
}