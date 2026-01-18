using Zenject;
using Utils;
using Data.Dto;
using System;
using UnityEngine;

namespace Core.Behaviors.Interaction
{
    public class DamageService : IDamageEmitService, IAttackNotifier, Providers.IProvider, IInternalEventReceiver
    {
        private int damage = 1;
        private readonly float distance = 100;
        private IExternalEventEmitter externalEventEmitter;

        public event Action OnAttack;

        public DamageService(int damage, float distance)
        {
            this.damage = damage;
            this.distance = distance;
        }

        [Inject]
        private void Construct(IExternalEventEmitter externalEventEmitter)
        {
            this.externalEventEmitter = Extensions.AssignWithNullCheck(externalEventEmitter);
        }

        public void EmitDamage()
        {
            externalEventEmitter.EmitEvent(new DamageData(damage), distance);
            OnAttack?.Invoke();
        }

        public void ReceiveEvent(IEvent @event)
        {
            if (@event is IDamageModifierData damageModifierData)
            {
                damage += damageModifierData.Damage;
                if(damage < 0) damage = 0;
            }
        }
    }
}