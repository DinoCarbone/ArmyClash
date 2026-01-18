using System;
using Core.Animations;
using Core.Behaviors.Interaction;
using Zenject;

namespace Core.Behaviors.Animations
{
    public class AttackAnimator : Providers.IProvider, IDisposable
    {
        private readonly IAnimation animation;

        private IAttackNotifier attackNotifier;

        public AttackAnimator(IAnimation animation)
        {
            this.animation = animation;
        }

        [Inject]
        private void Construct(IAttackNotifier attackNotifier)
        {
            this.attackNotifier = attackNotifier;
            attackNotifier.OnAttack += OnAttackHandle;
        }

        private void OnAttackHandle()
        {
            animation.Play();
        }

        public void Dispose()
        {
            animation.StopAnimation();
            attackNotifier.OnAttack -= OnAttackHandle;
        }
    }
}