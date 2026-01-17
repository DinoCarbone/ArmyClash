using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Core.Behaviors.Visuals
{
    public class ColliderResizer : IInternalEventReceiver, IProvider
    {
        private readonly Action<Vector3> resizeAction;
        private readonly float originalLocalBottomY;

        public ColliderResizer(Component colliderComponent)
        {
            if (colliderComponent == null) throw new ArgumentNullException(nameof(colliderComponent));

            originalLocalBottomY = CalculateLocalBottomY(colliderComponent);

            resizeAction = CreateResizeAction(colliderComponent);
        }

        private float CalculateLocalBottomY(Component component)
        {
            return component switch
            {
                BoxCollider box => box.center.y - box.size.y / 2f,
                SphereCollider sphere => sphere.center.y - sphere.radius,
                CapsuleCollider capsule => capsule.center.y - (capsule.height / 2f + capsule.radius),
                CharacterController character => character.center.y - character.height / 2f,
                _ => 0
            };
        }
        // BoxCollider - X меняет ширину, Y меняет высоту (фиксирует низ), Z меняет глубину
        // SphereCollider - X меняет диаметр (фиксирует низ), Y игнорируется, Z игнорируется  
        // CapsuleCollider - X меняет диаметр, Y меняет высоту (фиксирует низ), Z игнорируется
        // CharacterController - X меняет диаметр, Y меняет высоту (фиксирует низ), Z игнорируется
        // MeshCollider - X/Y/Z при любом значении выдают предупреждение
        private Action<Vector3> CreateResizeAction(Component colliderComponent)
        {
            return colliderComponent switch
            {
                BoxCollider box => size =>
                {
                    Vector3 newSize = box.size;
                    if (size.x != 0) newSize.x = size.x;
                    if (size.y != 0) newSize.y = size.y;
                    if (size.z != 0) newSize.z = size.z;

                    box.size = newSize;

                    if (size.y != 0)
                    {
                        box.center = new Vector3(box.center.x,
                            originalLocalBottomY + newSize.y / 2f,
                            box.center.z);
                    }
                }
                ,

                SphereCollider sphere => size =>
                {
                    if (size.x != 0)
                    {
                        float newRadius = size.x / 2f;
                        sphere.radius = newRadius;

                        sphere.center = new Vector3(sphere.center.x,
                            originalLocalBottomY + newRadius,
                            sphere.center.z);
                    }
                }
                ,

                CapsuleCollider capsule => size =>
                {
                    bool changedWidth = size.x != 0;
                    bool changedHeight = size.y != 0;

                    if (changedWidth || changedHeight)
                    {
                        float newRadius = changedWidth ? size.x / 2f : capsule.radius;
                        float newHeight = changedHeight ? size.y : capsule.height;

                        if (changedWidth) capsule.radius = newRadius;
                        if (changedHeight) capsule.height = newHeight;

                        capsule.center = new Vector3(capsule.center.x,
                            originalLocalBottomY + newHeight / 2f + newRadius,
                            capsule.center.z);
                    }
                }
                ,

                CharacterController character => size =>
                {
                    bool changedWidth = size.x != 0;
                    bool changedHeight = size.y != 0;

                    if (changedWidth || changedHeight)
                    {
                        float newRadius = changedWidth ? size.x / 2f : character.radius;
                        float newHeight = changedHeight ? size.y : character.height;

                        if (changedWidth) character.radius = newRadius;
                        if (changedHeight) character.height = newHeight;

                        character.center = new Vector3(0,
                            originalLocalBottomY + newHeight / 2f,
                            0);
                    }
                }
                ,

                MeshCollider mesh => size =>
                {
                    if (size.x != 0 || size.y != 0 || size.z != 0)
                        Debug.LogWarning("MeshCollider size cannot be changed directly");
                }
                ,

                _ => size =>
                {
                    if (size.x != 0 || size.y != 0 || size.z != 0)
                        Debug.LogWarning($"Unsupported component type: {colliderComponent.GetType().Name}");
                }
            };
        }

        public void ReceiveEvent(IEvent @event)
        {
            if (@event is IColladerChangeData changedEvent)
            {
                resizeAction?.Invoke(changedEvent.Size);
            }
        }
    }
}