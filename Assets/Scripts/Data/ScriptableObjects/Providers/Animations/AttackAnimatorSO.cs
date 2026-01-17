using System;
using Core.Animations;
using Core.Behaviors.Animations;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Animations
{
    [CreateAssetMenu(fileName = "AttackAnimator",
      menuName = "ScriptableObjects/Animations/AttackAnimator")]
    public class AttackAnimatorSO : BaseProviderSO
    {
        [SerializeField] private float speedAttack = 1f;
        [SerializeField] private float attackDistance = 1f;

        public override IProvider CreateProvider(params object[] dependencies)
        {
            Transform transform = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                transform = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }

            if (transform == null)
                throw new Exception($"EnemyAgentSO: Transform is empty");

            IAnimation animation = new BackAndForthAnimator(transform, speedAttack, attackDistance);

            return new AttackAnimator(animation);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Attack Point",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}