using System.Collections.Generic;
using Data.Dto;
using Utils;
using Zenject;

namespace Core.Behaviors.Interaction
{
    /// <summary>
    /// Маршрутизатор событий: собирает получателей событий в проекте и перенаправляет
    /// входящие анимационные и внутренние события соответствующим слушателям.
    /// </summary>
    public class EventRouter : Providers.IProvider, IInternalEventReceiverService
    {
        private List<IInternalEventReceiver> internalEventReceivers;

        [Inject]
        private void Construct(AllEntityData allEntityData)
        {
            AllEntityData entityData = Extensions.AssignWithNullCheck(allEntityData);
            AddInternalEventReceiver(entityData);
        }
        private void AddInternalEventReceiver(AllEntityData allEntityData)
        {
            internalEventReceivers = new List<IInternalEventReceiver>();
            foreach (object internalEventReceiver in allEntityData.EntityData)
            {
                if (internalEventReceiver is IInternalEventReceiver receiver)
                    internalEventReceivers.Add(receiver);
            }
        }

        /// <summary>Перенаправляет внутреннее событие всем зарегистрированным получателям.</summary>
        public void ReceiveEvent(IEvent @event)
        {
            foreach (IInternalEventReceiver internalEventReceiver in internalEventReceivers)
            {
                internalEventReceiver.ReceiveEvent(@event);
            }
        }
    }
}