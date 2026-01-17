using UnityEngine;
using Zenject;
using Utils;
using Data.Dto;

namespace Core.Behaviors.Interaction
{
    [DisallowMultipleComponent]
    public class ColliderExternalEventReceiver : MonoBehaviour, IExternalEventReceiver, Providers.IProvider
    {
        private IInternalEventReceiverService internalEventReceiverService;

        [Inject]
        private void Construct(IInternalEventReceiverService internalEventReceiverService)
        {
            this.internalEventReceiverService = Extensions.AssignWithNullCheck(internalEventReceiverService);
        }

        /// <summary>
        /// Перенаправляет полученное внешнее событие в локальный сервис получения внутренних событий.
        /// </summary>
        public void ReceiveEvent(IEvent @event)
        {
            internalEventReceiverService.ReceiveEvent(@event);
        }
    }
}