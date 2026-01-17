using System;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Attack
{
    [CreateAssetMenu(fileName = "DelayAttack",
     menuName = "ScriptableObjects/States/Attack/DelayAttack")]
    public class DelayAttackBehaviorSO : BehaviorSO<DelayAttack>
    {
        [Tooltip("Delay before attack")]
        [SerializeField] private float delay;
        public override IState CreateConfigState(params object[] contexts)
        {
            return new DelayAttack(GetIncompatibleTypes(), delay);
        }
        /// <summary>Создаёт конфигурацию состояния атаки и возвращает базовый тип поведения.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}