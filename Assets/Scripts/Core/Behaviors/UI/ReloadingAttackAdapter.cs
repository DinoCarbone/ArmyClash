using System;
using Core.Behaviors.Interaction;
using UnityEngine;
using Zenject;

namespace Core.Behaviors.UI
{
    public class ReloadingAttackAdapter : Providers.IProvider, IDisposable
    {
        private IDelayAttackProvider delayAttackProvider;
        private readonly IValueDisplay reloadingDisplay;

        public ReloadingAttackAdapter(IValueDisplay reloadingDisplay)
        {
            this.reloadingDisplay = reloadingDisplay;

            reloadingDisplay.SetMaxValue(100);
            ResetValue();
        }

        [Inject]
        private void Construct(IDelayAttackProvider delayAttackProvider)
        {
            this.delayAttackProvider = delayAttackProvider;

            delayAttackProvider.OnBreak += ResetValue;
            delayAttackProvider.OnUpdateProgress += OnUpdateProgressHandle;
        }

        private void OnUpdateProgressHandle(float progress)
        {
            reloadingDisplay.DisplayValue((int)(progress * 100f));
        }

        private void ResetValue()
        {
            reloadingDisplay.DisplayValue(0);
        }

        public void Dispose()
        {
            delayAttackProvider.OnBreak -= ResetValue;
            delayAttackProvider.OnUpdateProgress -= OnUpdateProgressHandle;
        }
    }
}