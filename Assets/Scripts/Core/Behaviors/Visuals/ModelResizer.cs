using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Core.Behaviors.Visuals
{
    public class ModelResizer : IInternalEventReceiver, IProvider
    {
        private readonly Transform transform;

        public ModelResizer(Transform transform)
        {
            this.transform = transform;
        }

        public void ReceiveEvent(IEvent @event)
        {
            if (@event is IScaleChandeData scaleChandeData)
            {
                transform.localScale = new Vector3(scaleChandeData.Scale, scaleChandeData.Scale, scaleChandeData.Scale);
                // предполагается, что родитель всегда изначально в Vector3.One
                Debug.Log($"New scale model: {scaleChandeData.Scale}");
            }
        }
    }
}