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
        protected CharacterController controller;

        public CharacterControllerMovementState(CharacterController controller, List<Type> incompatibleStates,
        float startSpeed) : base(incompatibleStates, startSpeed)
        {
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }

        protected override void OnMove(Vector2 axis)
        {
            var move = new Vector3(axis.x, 0, axis.y);
            move.Normalize();
            controller.Move(move * speed * Time.deltaTime);
            controller.transform.position = new Vector3(controller.transform.position.x, 0, controller.transform.position.z);
        }
    }
}