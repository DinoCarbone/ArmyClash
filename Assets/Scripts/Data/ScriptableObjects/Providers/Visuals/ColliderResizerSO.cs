using System;
using Core.Behaviors.Visuals;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Visuals
{
    [CreateAssetMenu(fileName = "ColliderResizer",
      menuName = "ScriptableObjects/Providers/Visuals/ColliderResizer")]
    public class ColliderResizerSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Collider controller = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                controller = dependencies[0] as Collider ?? (dependencies[0] as GameObject)?.GetComponent<Collider>();
            }
            if(controller == null)
                throw new Exception("ColliderExternalEventReceiverSO: collider is empty");

            return new ColliderResizer(controller);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Collider",
                    typeName = "UnityEngine.Collider, UnityEngine",
                    optional = false
                }
            };
        }
    }
}