using UnityEngine;

namespace Data.Dto
{
    public interface IEvent { }

    /// <summary>Событие урона — предоставляет величину урона.</summary>
    public interface IDamageData : IEvent
    {
        int Damage { get; }
    }
    public interface IDamageModifierData : IEvent
    {
        int Damage { get; }
    }
    public interface IHealthModifierData : IEvent
    {
        int Health { get; }
    }
    public interface IModelChangeData : IEvent
    {
        GameObject Prefub { get; }
    }
    public interface IColladerChangeData : IEvent
    {
        Vector3 Size { get; }
    }
    public interface IScaleChandeData : IEvent
    {
        float Scale { get; }
    }
}