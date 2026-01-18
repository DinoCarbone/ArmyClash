using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Core.Behaviors.Visuals
{
    public class ModelSwitcher : IInternalEventReceiver, IProvider, IModelSwitchNotifyer
    {
        private readonly Transform parentModel;

        public event System.Action<GameObject> OnModelSwitch;

        public ModelSwitcher(Transform parentModel)
        {
            this.parentModel = parentModel;
        }

        public void ReceiveEvent(IEvent @event)
        {
            if(@event is  IModelChangeData switchModelEvent)
            {
                SwitchModel(switchModelEvent.Prefub);
            }
        }
        private void SwitchModel(GameObject prefub)
        {
            foreach (Transform child in parentModel)
            {
                Object.Destroy(child.gameObject);
            }

            GameObject model = Object.Instantiate(prefub, parentModel);
            model.transform.localPosition = Vector3.zero; // на всякий случай :3

            OnModelSwitch?.Invoke(model);
        }
    }
}