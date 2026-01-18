using System;
using Core.Providers;
using Core.Simulation;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Agents
{
    public class EntityAgent : MovementAttackAgent, ICombatUnit, IDisposable
    {
        private IDeathProvider deathProvider;
        private Transform _targetTransform;

        public event Action<IUnit> OnDeath;

        protected override Transform targetTransform => _targetTransform;

        public Transform Transform => selfTransform;

        public EntityAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
            : base(selfTransform, attackAngleThreshold, attackDistance)
        {
        }
        
        [Inject]
        private void Construct(IDeathProvider deathProvider)
        {
            this.deathProvider = deathProvider;
            this.deathProvider.OnDie += HandleDeath;
        }

        private void HandleDeath()
        {
            OnDeath?.Invoke(this);
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        public void Dispose()
        {
            deathProvider.OnDie -= HandleDeath;
        }
    }
}