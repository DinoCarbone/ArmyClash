using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using Core.Services.States;
using Data.Dto;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.States.Movement
{
    /// <summary>
    /// Базовый класс для состояний движения.
    /// </summary>
    /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
    public class BaseMovement : BaseIncompatible
    {
        public BaseMovement(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    /// <summary>
    /// Базовый класс для осевого движения.
    /// </summary>
    public abstract class BaseAxisMovement : BaseMovement, IUpdateState, IEnterState, IExitState, IInternalEventReceiver
    {
        protected IAxisMovementProvider inputAxisProvider;
        protected float speed;

        public event Action OnEnter;
        public event Action OnExit;

        public bool CanEnter => inputAxisProvider.IsHandle;

        public bool CanExit => !inputAxisProvider.IsHandle;
        protected BaseAxisMovement(List<Type> incompatibleStates, float startSpeed) : base(incompatibleStates)
        {
            speed = startSpeed;
        }
        [Inject]
        public void Construct(IAxisMovementProvider inputAxisProvider)
        {
            this.inputAxisProvider = Utils.Extensions.AssignWithNullCheck(inputAxisProvider);
        }
        public void Update()
        {
            OnMove(inputAxisProvider.Axis);
        }
        protected abstract void OnMove(Vector2 axis);
        public void Enter()
        {
            OnEnter?.Invoke();
        }
        public void Exit()
        {
            OnExit?.Invoke();
        }

        public void ReceiveEvent(IEvent @event)
        {
            if (@event is ISpeedModifierData speedModifierData)
            {
                speed += speedModifierData.Speed;
                if(speed < 0) speed = 0;
            }
        }
    }
}