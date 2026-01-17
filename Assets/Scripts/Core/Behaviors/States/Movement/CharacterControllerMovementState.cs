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
        protected float speed = 5f;
        protected CharacterController controller;

        public CharacterControllerMovementState(CharacterController controller, List<Type> incompatibleStates, float startSpeed = 5f) : base(incompatibleStates)
        {
            speed = startSpeed;
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }

        protected override void OnMove(Vector2 axis)
        {
            // Vector3 move = controller.transform.forward * axis.y +
            //                controller.transform.right * axis.x;

            // move.y = 0;
            var move = new Vector3(axis.x, 0, axis.y);

            if (move.magnitude > MagnitudeThreshold)
            {
                move.Normalize();
                controller.Move(move * speed * Time.deltaTime);
            }
        }
    }
}