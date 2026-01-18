using System;
using Core.Behaviors.Interaction;
using Core.Behaviors.Lifecycle;
using Core.Providers;
using Data.Dto;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Health
{
    public class HealthService : IInternalEventReceiver, IDamageProvider, IHealthProvider, IDeathProvider
    {
        private readonly GameObject coreObject;
        private IDestructionService destructionService;

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public event Action<int> OnTakeDamage;
        public event Action<int> OnChangeHealth;
        public event Action<int> OnChangeMaxHealth;
        public event Action OnDie;

        public HealthService(int maxHealth, GameObject coreObject)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            this.coreObject = coreObject;
        }

        [Inject]
        private void Construct(IDestructionService destructionService)
        {
            this.destructionService = destructionService;
        }
        
        public void ReceiveEvent(IEvent @event)
        {
            if(@event is IDamageData damageData)
            {
                ReceiveDamage(damageData.Damage);
            }
            if(@event is IHealthModifierData damageModifierData)
            {
                MaxHealth += damageModifierData.Health;
                Health = MaxHealth;
                if(MaxHealth <= 0)
                {
                    MaxHealth = 0;
                    OnChangeHealth?.Invoke(0);
                    DeathEmit();
                    
                    Debug.LogWarning($"Died due to negative max health: {MaxHealth}");
                    return;
                }
                OnChangeMaxHealth?.Invoke(MaxHealth);
                OnChangeHealth?.Invoke(Health);
            }
        }
        private void ReceiveDamage(int damage)
        {
            if(damage <= 0) return;

            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                DeathEmit();
            }
            OnTakeDamage?.Invoke(damage);
            OnChangeHealth?.Invoke(Health);
        }
        private void DeathEmit()
        {
            OnDie?.Invoke();
            destructionService.Destruct(coreObject);
        }
    }
}