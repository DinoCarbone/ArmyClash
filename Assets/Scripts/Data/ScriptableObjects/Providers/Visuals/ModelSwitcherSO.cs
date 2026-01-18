using System;
using Core.Behaviors.Visuals;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Visuals
{
    [CreateAssetMenu(fileName = "ModelSwitcher",
      menuName = "ScriptableObjects/Providers/Visuals/ModelSwitcher")]
    public class ModelSwitcherSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Transform rootTransform = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                rootTransform = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }
            if(rootTransform == null)
            throw new Exception($"TransformRotationBehaviorSO: Transform is empty.");

            return new ModelSwitcher(rootTransform);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "View Parent",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}