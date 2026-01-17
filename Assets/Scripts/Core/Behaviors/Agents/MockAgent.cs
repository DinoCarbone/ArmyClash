using Core.Providers;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Agents
{
    public class MockAgent : MovementAttackAgent
    {
        private Transform _targetTransform;
        protected override Transform targetTransform => _targetTransform;

        public MockAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
            : base(selfTransform, attackAngleThreshold, attackDistance)
        {
        }

        /// <summary>
        /// Выполняет внедрение провайдера сцены игрока и устанавливает цель агента.
        /// </summary>
        [Inject]
        private void Construct(ITargetSceneProvider playerSceneProvider)
        {
            _targetTransform = playerSceneProvider.Transform;
        }
    }
}