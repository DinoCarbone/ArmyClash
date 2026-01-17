using UnityEngine;

namespace Data.Dto
{
    public interface IEvent { }

    /// <summary>Событие урона — предоставляет величину урона.</summary>
    public interface IDamageEvent : IEvent
    {
        int Damage { get; }
    }
}