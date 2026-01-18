using System;
using Core.Behaviors.Health;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Health
{
    [CreateAssetMenu(fileName = "HealthService",
      menuName = "ScriptableObjects/Providers/Health/HealthService")]
    public class HealthServiceSO : BaseProviderSO
    {
        [SerializeField, Tooltip("Maximum health value for the health service.")]
        private int maxHealth = 100;

        public override IProvider CreateProvider(params object[] dependencies)
        {
            GameObject coreObject = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                coreObject = dependencies[0] as GameObject ?? (dependencies[0] as GameObject)?.GetComponent<GameObject>();
            }
            if(coreObject == null)
                throw new Exception("ColliderExternalEventReceiverSO: collider is empty");
                
            return new HealthService(maxHealth, coreObject);
        }
        
        public override ContextRequirement[] GetContextRequirements()
        {
            return new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Core Object",
                    typeName = "UnityEngine.GameObject, UnityEngine",
                    optional = false
                }
            };
        }
    }
}