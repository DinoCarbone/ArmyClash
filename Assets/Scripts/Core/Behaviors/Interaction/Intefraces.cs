using System;
using Data.Dto;

namespace Core.Behaviors.Interaction
{

    /// <summary>Сервис приёма внутренних событий и маршрутизации их в систему сущности.</summary>
    public interface IInternalEventReceiverService
    {
        /// <summary>Принимает внутреннее событие типа <see cref="IEvent"/>.</summary>
        void ReceiveEvent(IEvent @event);
    }

    /// <summary>Интерфейс для компонентов, которые могут получать внутренние события.</summary>
    public interface IInternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }
    
    public interface IAttackNotifier
    {
        event Action OnAttack;
    }

    public interface IDelayAttackProvider
    {
        event Action<float> OnUpdateProgress;
        event Action OnBreak;
    }

    public interface IDamageEmitService
    {
        void EmitDamage();
    }

    /// <summary>Излучатель внешних событий (например, столкновений или радиусных эффектов).</summary>
    public interface IExternalEventEmitter
    {
        void EmitEvent(IEvent @event, float distance);
    }

    /// <summary>Получатель внешних событий.</summary>
    public interface IExternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }
}