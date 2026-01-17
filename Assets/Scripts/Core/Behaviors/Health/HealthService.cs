using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Core.Behaviors.Health
{
    public class HealthService : IInternalEventReceiver, IDamageProvider, IHealthProvider, IDeathProvider
    {
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public event Action<int> OnTakeDamage;
        public event Action<int> OnChangeHealth;
        public event Action<int> OnChangeMaxHealth;
        public event Action OnDie;

        public HealthService(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }
        
        public void ReceiveEvent(IEvent @event)
        {
            if(@event is IDamageData damageData)
            {
                ReceiveDamage(damageData.Damage);
            }
            if(@event is IHealthModifierData damageModifierData)
            {
                Health += damageModifierData.Health;
                OnChangeMaxHealth?.Invoke(Health);
                Debug.Log($"New health: {Health}");
            }
        }
        private void ReceiveDamage(int damage)
        {
            if(damage <= 0) return;

            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                OnDie?.Invoke();
            }
            OnTakeDamage?.Invoke(damage);
            OnChangeHealth?.Invoke(Health);
        }
    }
}