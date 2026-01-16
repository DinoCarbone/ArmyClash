using Zenject;
using Utils;

namespace Core.Behaviors.Interaction
{
    /// <summary>
    /// Обработчик события анимации, который при совпадении события производит эмиссию события нанесения урона.
    /// </summary>
    public class DamageDealer : Providers.IProvider
    {
        private readonly float radius = 1;
        private readonly int damage = 1;
        private readonly float distance = 100;
        IExternalEventEmitter externalEventEmitter;

        public DamageDealer(int damage, float distance, float radius)
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

        /// <summary>
        /// Вызывается при наступлении анимационного события; при совпадении — эмитит внутреннее DamageData.
        /// </summary>
        // public void ReceiveAnimationEvent(AnimationEventSO animationEvent)
        // {
        //     if (this.animationEvent == animationEvent)
        //     {
        //         externalEventEmitter.EmitEvent(new DamageData(damage), distance, radius);
        //     }
        // }
    }
}