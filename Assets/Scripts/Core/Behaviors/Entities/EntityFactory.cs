using Core.Behaviors.Interaction;
using Data.Dto;
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

        public GameObject Create(Vector3 position, Quaternion quaternion, params BaseMultiConfig[] configs)
        {
            GameObject entity = container.InstantiatePrefab(entityPrefub, position, quaternion, null);
            IExternalEventReceiver receiver = entity.GetComponentInChildren<IExternalEventReceiver>();

            if (receiver != null)
            {
                if (configs != null && configs.Length > 0)
                {
                    foreach (var config in configs)
                    {
                        if (config != null)
                        {
                            foreach (IEvent @event in config.GetConfigs())
                            {
                                if(@event != null) receiver.ReceiveEvent(@event);
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Receiver not found");
            }
            return entity;
        }
    }
    public interface IEntityFactory
    {
        GameObject Create(Vector3 position, Quaternion quaternion, params BaseMultiConfig[] configs);
    }
}