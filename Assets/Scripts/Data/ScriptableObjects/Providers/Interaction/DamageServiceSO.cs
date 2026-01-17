using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "DamageService",
      menuName = "ScriptableObjects/Providers/Interactions/DamageService")]
    public class DamageServiceSO : BaseProviderSO
    {

        [SerializeField, Tooltip("Damage amount dealt by this dealer.")]
        private int damage = 1;

        [SerializeField, Tooltip("Maximum distance at which this attack can hit.")]
        private float distance = 1f;

        /// <summary>Создаёт провайдер `DamageDealer` с заданными параметрами.</summary>
        public override IProvider CreateProvider(params object[] _)
        {
            return new DamageService(damage, distance);
        }
    }
}