using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "TransformMovement",
    menuName = "ScriptableObjects/States/Movement/TransformMovement")]
    public class TransformMovementStateSO : BehaviorSO<TransformMovementState>
    {
        [SerializeField] private float speed = 5f;
        
        public override IState CreateConfigState(params object[] dependencies)
        {
            Transform rootTransform = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                rootTransform = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }
            if(rootTransform == null)
            throw new Exception($"TransformRotationBehaviorSO: Transform is empty.");

            return new TransformMovementState(rootTransform, GetIncompatibleTypes(), speed);
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseMovement);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Core Transform",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}