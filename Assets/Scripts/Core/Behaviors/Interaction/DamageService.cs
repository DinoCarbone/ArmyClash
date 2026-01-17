using Zenject;
using Utils;
using Data.Dto;

namespace Core.Behaviors.Interaction
{
    public class DamageService : IDamageService, Providers.IProvider
    {
        private readonly float radius = 1;
        private readonly int damage = 1;
        private readonly float distance = 100;
        private IExternalEventEmitter externalEventEmitter;

        public DamageService(int damage, float distance, float radius)
        {
            this.damage = damage;
            this.distance = distance;
            this.radius = radius;
        }

        [Inject]
        private void Construct(IExternalEventEmitter externalEventEmitter)
        {
            this.externalEventEmitter = Extensions.AssignWithNullCheck(externalEventEmitter);
        }

        public void EmitDamage()
        {
            externalEventEmitter.EmitEvent(new DamageData(damage), distance, radius);
        }
    }
}