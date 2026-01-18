using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Services.States;
using Data.Dto;
using UnityEngine;
using Utils;

namespace Core.Behaviors.States.Attack
{
    /// <summary>
    /// Реализация поведения атаки с задержкой при помощи таймера. Update вызывается извне.
    /// </summary>
    public class DelayAttack : SimpleAttack, IUpdateState, IDelayAttackProvider, IInternalEventReceiver
    {
        private const float delayDuration = 10f;
        private float speed;
        private readonly Timer timer = new Timer();

        public event Action<float> OnUpdateProgress;
        public event Action OnBreak;

        public DelayAttack(List<Type> incompatibleStates, float speed) : base(incompatibleStates)
        {
            this.speed = speed;
        }

        protected override void OnEnterHandle()
        {
            timer.Start(delayDuration, onUpdateProgress: (progress)=> OnUpdateProgress?.Invoke(progress),
             onComplete: OnCompleteHandle);
        }
        private void OnCompleteHandle()
        {
            EmitDamage();
            CanExit = true;
        }
        public void Update()
        {
            timer.Update(Time.deltaTime * speed);
        }
        protected override void OnExitHandle()
        {
            timer.Reset();
            OnBreak?.Invoke();
        }

        public void ReceiveEvent(IEvent @event)
        {
            if(@event is IAttackSpeedModifierData attackSpeedModifierData)
            {
                speed += attackSpeedModifierData.AttackSpeed;
                if(speed < 0f) speed = 0f;
            }
        }
    }
}