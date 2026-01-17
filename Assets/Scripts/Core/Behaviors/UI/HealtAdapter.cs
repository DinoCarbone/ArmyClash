using Core.Behaviors.Health;
using Zenject;

namespace Core.Behaviors.UI
{
    public class HealtAdapter : Providers.IProvider
    {
        private IHealthProvider healthProvider;
        private readonly IValueDisplay reloadingDisplay;

        public HealtAdapter(IValueDisplay reloadingDisplay)
        {
            this.reloadingDisplay = reloadingDisplay;
        }

        [Inject]
        private void Construct(IHealthProvider healthProvider)
        {
            this.healthProvider = healthProvider;

            reloadingDisplay.SetMaxValue(healthProvider.MaxHealth);
            healthProvider.OnChangeHealth += OnChangeHealthHandle;
        }

        private void OnChangeHealthHandle(int health)
        {
            reloadingDisplay.DisplayValue(health);
        }

        public void Dispose()
        {
            healthProvider.OnChangeHealth -= OnChangeHealthHandle;
        }
    }
}