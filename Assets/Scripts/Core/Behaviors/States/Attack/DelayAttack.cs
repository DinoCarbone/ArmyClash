using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Services.States;
using UnityEngine;
using Utils;

namespace Core.Behaviors.States.Attack
{
    /// <summary>
    /// Реализация поведения атаки с задержкой при помощи таймера. Update вызывается извне.
    /// </summary>
    public class DelayAttack : SimpleAttack, IUpdateState, IDelayAttackProvider
    {
        private readonly float delay;
        private readonly Timer timer = new Timer();

        public event Action<float> OnUpdateProgress;
        public event Action OnBreak;

        public DelayAttack(List<Type> incompatibleStates, float delay) : base(incompatibleStates)
        {
            this.delay = delay;
        }

        protected override void OnEnterHandle()
        {
            timer.Start(delay, onUpdateProgress: (progress)=> OnUpdateProgress?.Invoke(progress),
             onComplete: OnCompleteHandle);
        }
        private void OnCompleteHandle()
        {
            EmitDamage();
            CanExit = true;
        }
        public void Update()
        {
            timer.Update(Time.deltaTime);
        }
        protected override void OnExitHandle()
        {
            timer.Reset();
            OnBreak?.Invoke();
        }
    }
}