using Zenject;
using Utils;
using Data.Dto;
using System;

namespace Core.Behaviors.Interaction
{
    public class DamageService : IDamageEmitService, IAttackNotifier, Providers.IProvider
    {
        private readonly int damage = 1;
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
    }
}