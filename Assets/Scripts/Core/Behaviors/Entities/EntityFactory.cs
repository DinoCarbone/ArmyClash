using Core.Behaviors.Interaction;
using Data.ScriptableObjects.Configs;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Entities
{
    public class EntityFactory : IEntityFactory
    {
        private readonly DiContainer container;
        private readonly GameObject entityPrefub;

        public EntityFactory(GameObject entityPrefub, DiContainer container)
        {
            this.entityPrefub = entityPrefub;
            this.container = container;
        }

        public void Create(params BaseEventConfigSO[] configs)
        {
            GameObject entity = container.InstantiatePrefab(entityPrefub);
            IExternalEventReceiver receiver = entity.GetComponentInChildren<IExternalEventReceiver>();

            if (receiver != null)
            {
                if (configs != null && configs.Length > 0)
                {
                    foreach (var config in configs)
                    {
                        if (config != null)
                        {
                            receiver.ReceiveEvent(config.GetConfig());
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Receiver not found");
            }
        }
    }
    public interface IEntityFactory
    {
        void Create(params BaseEventConfigSO[] configs);
    }
}