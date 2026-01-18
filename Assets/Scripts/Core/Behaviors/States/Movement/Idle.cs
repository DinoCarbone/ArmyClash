using System;
using System.Collections.Generic;
using Core.Services.States;

namespace Core.Behaviors.States.Movement
{
    /// <summary>
    /// Базовый тип для idle-состояний, содержит список несовместимых состояний.
    /// </summary>
    public class BaseIdle : BaseIncompatible
    {
        public BaseIdle(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    /// <summary>
    /// Cостояние пустого простоя.
    /// </summary>
     public class SimpleIdle : BaseIdle, IEnterState, IExitState
    {
        public SimpleIdle(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
        public bool CanEnter => true;
        public bool CanExit => false;

        public event Action OnEnter;
        public event Action OnExit;

        public void Enter()
        {
            OnEnter?.Invoke();
        }
        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}