using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class TransformMovementState : BaseAxisMovement
    {
        private const float MagnitudeThreshold = 0.1f;
        private Transform transform;

        public TransformMovementState(Transform transform, List<Type> incompatibleStates,
            float startSpeed) : base(incompatibleStates, startSpeed)
        {
            this.transform = transform;
        }

        protected override void OnMove(Vector2 axis)
        {
            var move = new Vector3(axis.x, 0, axis.y);

            if (move.magnitude > MagnitudeThreshold)
            {
                move.Normalize();
                transform.position += move * speed * Time.deltaTime;
            }
        }
    }
}