using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using Core.Services.States;
using Zenject;

namespace Core.Behaviors.States.Attack
{
    public class BaseAttack : BaseIncompatible
    {
        public BaseAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }
    }
    /// <summary>
    /// Базовая реализация поведения атаки с обработкой входа/выхода.
    /// </summary>
    public abstract class SimpleAttack : BaseAttack, IEnterState, IExitState
    {
        private IDamageEmitService damageService;
        private IAttackProvider attackProvider;

        public SimpleAttack(List<Type> incompatibleStates) : base(incompatibleStates)
        {
        }

        [Inject]
        public void Construct(IAttackProvider attackProvider, IDamageEmitService damageService)
        {
            this.damageService = damageService;
            this.attackProvider = attackProvider;
        }

        public bool CanExit { get; protected set; }
        public bool CanEnter => attackProvider.IsAttack;

        public event Action OnExit;
        public event Action OnEnter;

        public void Enter()
        {
            OnEnter?.Invoke();
            OnEnterHandle();
        }

        protected void EmitDamage()
        {
            damageService.EmitDamage();
        }

        protected abstract void OnEnterHandle();

        public void Exit()
        {
            OnExitHandle();
            CanExit = false;
            OnExit?.Invoke();
        }

        protected abstract void OnExitHandle();
    }
}