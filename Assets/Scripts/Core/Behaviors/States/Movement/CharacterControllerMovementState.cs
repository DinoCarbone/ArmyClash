using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    /// <summary>
    /// Класс состояния движения на основе <see cref="CharacterController"/>.
    /// </summary>
    public class CharacterControllerMovementState : BaseAxisMovement
    {
        private const float MagnitudeThreshold = 0.1f;
        protected CharacterController controller;

        public CharacterControllerMovementState(CharacterController controller, List<Type> incompatibleStates,
        float startSpeed) : base(incompatibleStates, startSpeed)
        {
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }

        protected override void OnMove(Vector2 axis)
        {
            var move = new Vector3(axis.x, 0, axis.y);

            if (move.magnitude > MagnitudeThreshold)
            {
                move.Normalize();
                controller.Move(move * speed * Time.deltaTime);
            }
        }
    }
}