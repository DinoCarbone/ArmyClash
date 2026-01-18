using System;
using Core.Behaviors.Interaction;
using Data.Dto;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.Visuals
{
    public class MaterialSwitcher : IInternalEventReceiver, Providers.IProvider, IDisposable
    {
        private MeshRenderer meshRenderer;
        private Material material;
        private IModelSwitchNotifyer modelSwitchNotifyer;

        public MaterialSwitcher(MeshRenderer meshRenderer)
        {
            this.meshRenderer = meshRenderer;
            if(meshRenderer != null) material = meshRenderer.material;
        }
        [Inject]
        private void Construct(IModelSwitchNotifyer modelSwitchNotifyer)
        {
            if(modelSwitchNotifyer != null)
            {
                this.modelSwitchNotifyer = modelSwitchNotifyer;
                this.modelSwitchNotifyer.OnModelSwitch += OnModelSwithHandle;
            }
        }

        private void OnModelSwithHandle(GameObject gameObject)
        {
            
            meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
            if(meshRenderer != null)
            {
                if(material != null)
                {
                    meshRenderer.material = material;
                    Debug.Log("OnModelSwithHandl");
                }
                else Debug.LogWarning("No material to apply to new model.");
            }
            else
            {
                Debug.LogWarning("No MeshRenderer found in new model to apply material.");
            }
        }
        public void ReceiveEvent(IEvent @event)
        {
            if(@event is IMaterialChangeData materialChangeData)
            {
                material = materialChangeData.Material;
                if(meshRenderer != null)
                {
                    meshRenderer.material = material;
                    Debug.Log($"New material applied: {material.name}");
                }
                else Debug.LogWarning("No MeshRenderer found to apply material.");
            }
        }

        public void Dispose()
        {
            if(modelSwitchNotifyer != null) modelSwitchNotifyer.OnModelSwitch += OnModelSwithHandle;
        }
    }
}