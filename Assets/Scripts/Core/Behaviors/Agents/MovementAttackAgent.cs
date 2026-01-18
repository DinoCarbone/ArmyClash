using Core.Providers;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Agents
{
    /// <summary>
    /// Базовый агент, объединяющий логику движения и атаки относительно целевого трансформа.
    /// Предоставляет свойства для определения, должен ли агент двигаться или атаковать, и вычисляет поворот.
    /// </summary>
    public abstract class MovementAttackAgent : IAttackProvider, IAxisMovementProvider, IAxisRotationProvider
    {
        protected readonly Transform selfTransform;
        
        private readonly float attackAngleThreshold;
        private readonly float attackDistance;

        protected abstract Transform targetTransform { get; }

        public bool IsAttack => CalculateIsAttack();
        public bool IsHandle => CalculateIsHandle();
        public Vector2 Axis => CalculateMovementAxis();
        public Quaternion Rotation => CalculateRotation();

        public MovementAttackAgent(Transform selfTransform, float attackAngleThreshold, float attackDistance)
        {
            this.selfTransform = Extensions.AssignWithNullCheck(selfTransform);
            this.attackAngleThreshold = attackAngleThreshold;
            this.attackDistance = attackDistance;
        }

        private Vector3 GetAgentPosition()
        {
            return selfTransform?.position ?? Vector3.zero;
        }

        private Quaternion GetAgentRotation()
        {
            return selfTransform?.rotation ?? Quaternion.identity;
        }

        private bool CalculateIsAttack()
        {
            if(targetTransform == null) return false;
            var distance = Vector3.Distance(GetAgentPosition(), targetTransform.position);
            return distance <= attackDistance && IsLookingAtPlayer();
        }

        private bool CalculateIsHandle()
        {
            if(targetTransform == null) return false;
            var distance = Vector3.Distance(GetAgentPosition(), targetTransform.position);
            return distance > attackDistance;
        }

        private Vector2 CalculateMovementAxis()
        {
            if (!IsHandle) return Vector2.zero;
            Vector3 target = (targetTransform.position - GetAgentPosition()).normalized;
            return new Vector2(target.x, target.z);
        }

        private Quaternion CalculateRotation()
        {
            if(targetTransform == null) return default;
                
            var direction = (targetTransform.position - GetAgentPosition()).normalized;
            if (direction == Vector3.zero) return GetAgentRotation();

            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            var currentRotation = GetAgentRotation();

            var angle = Quaternion.Angle(currentRotation, targetRotation);
            if (angle <= attackAngleThreshold)
            {
                return targetRotation;
            }

            return Quaternion.RotateTowards(currentRotation, targetRotation, attackAngleThreshold);
        }

        private bool IsLookingAtPlayer()
        {
            var directionToPlayer = (targetTransform.position - GetAgentPosition()).normalized;
            var forward = selfTransform?.forward ?? Vector3.forward;

            var angle = Vector3.Angle(forward, directionToPlayer);
            return angle <= attackAngleThreshold;
        }
    }
}