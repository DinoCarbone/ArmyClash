using System;
using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Маркер интерфейса данных, описывающих убиваемый объект.
    /// </summary>
    public interface IKillableData
    {
        public GameObject CoreGameObject { get; }
    }

    /// <summary>
    /// Сервис регистрации смертей сущностей.
    /// </summary>
    public interface IDeathService
    {
        /// <summary>Регистрирует смерть по переданным данным.</summary>
        /// <param name="killable">Данные убиваемого объекта.</param>
        void RegisterDeath(IKillableData killable);
    }

    /// <summary>
    /// Менеджер спавна/деспавна врагов.
    /// </summary>
    public interface IEnemySpawner
    {
        /// <summary>Создаёт сущность врага.</summary>
        void Spawn();

        /// <summary>Удаляет сущность врага.</summary>
        /// <param name="gameObject">GameObject, подлежащий удалению.</param>
        void Despawn(GameObject gameObject);
    }

    /// <summary>
    /// Фабрика создания врагов.
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>Создаёт и возвращает GameObject сущности.</summary>
        public GameObject Create();
    }
}